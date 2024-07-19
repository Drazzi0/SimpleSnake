using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    private Vector2 _direction = Vector2.right;
    public List<Transform> _segments;
    private bool correctDirection = true;
    public int initialSize = 4;
    private float timeChange;

    private void Start ()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        //Time.fixedDeltaTime = 0.06f;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W)) && (correctDirection == true )) {
            _direction = Vector2.up;
            correctDirection = false;
        } else if ((Input.GetKeyDown(KeyCode.S)) && (correctDirection == true )) {
            _direction = Vector2.down;
            correctDirection = false;
        } else if ((Input.GetKeyDown(KeyCode.A)) && (correctDirection == false )) {
            _direction = Vector2.left;
            correctDirection = true;
        } else if ((Input.GetKeyDown(KeyCode.D)) && (correctDirection == false )) {
            _direction = Vector2.right;
            correctDirection = true;
        }
    }
    private void DecreaseSpeed(){
        Time.fixedDeltaTime += 0.002f;
    }

    private void IncreaseSpeed(){
        Time.fixedDeltaTime -= 0.002f;
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i-1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void Grow(int i)
    {
        for(int x = 0; x < i; x++){
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = _segments[_segments.Count-1].position;

            _segments.Add(segment);
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
        ScoreManager.instance.ResetPoints();
        SpawnManager.instance.ResetInstantitatedObjects();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food") {
            Grow(1);
        } else if (other.tag =="SuperFood") {
            Grow(5);
        } else if (other.tag =="Obstacle") {
            ResetState();
        } else if (other.tag =="SpeedUp") {
            IncreaseSpeed();
        } else if (other.tag =="SpeedDown") {
            DecreaseSpeed();
        }
    }
}
