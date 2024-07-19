using UnityEngine;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour
{
    public Snake snakeInstance;
    public BoxCollider2D gridArea;
    public GameObject Food, SuperFood, BuffSpeedUp, BuffSpeedDown;
    public static SpawnManager instance;
    private GameObject food, superFood, buffSpeedUp, buffSpeedDown;
    //private List<Vector2> objectPosition = new List<Vector2> {};
    private Vector2[] objectPosition = 
    {
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0),
        new Vector2(0, 0)
    };
    private List<bool> didItSpawnArleady = new List<bool> {false, false, false};
    public float minSpawnTime = 5, maxSpawnTime = 10;
    private float randomTime;

    private void Awake(){
        instance = this;
    }
    private void Start(){
        food = Spawner(Food, 0);
        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
    private void Update(){
        randomTime -= Time.deltaTime;
        //Debug.Log(randomTime);

        if (SpawnTimer(randomTime)){
            int temp = Random.Range(1, 4);

            switch(temp){
                case 1:
                    if(superFood){
                        Destroyer(temp);
                        superFood = Spawner(SuperFood, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    } else {
                        superFood = Spawner(SuperFood, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    }
                break;
                case 2:
                    if(buffSpeedUp){
                        Destroyer(temp);
                        buffSpeedUp = Spawner(BuffSpeedUp, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    } else {
                        buffSpeedUp = Spawner(BuffSpeedUp, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    }
                break;
                case 3:
                    if(buffSpeedDown){
                        Destroyer(temp);
                        buffSpeedDown = Spawner(BuffSpeedDown, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    } else {
                        buffSpeedDown = Spawner(BuffSpeedDown, temp);
                        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    }
                break;
            }
        }
    }
    private bool SpawnTimer(float timing){
        if (timing < 0){
            return true;
        }
        return false;
    }
    private GameObject Spawner(GameObject spawnElement, int i){
        Bounds bounds = this.gridArea.bounds;

        objectPosition[i] = GetRandomPosition(bounds);

        while(IsInsideSnake(objectPosition[i])){
            objectPosition[i] = GetRandomPosition(bounds);
        }
        GameObject spawned = Instantiate(spawnElement, new Vector3(Mathf.Round(objectPosition[i].x), Mathf.Round(objectPosition[i].y), 0.0f), Quaternion.identity);
        return spawned;
    }
    public void Destroyer(int i){
        switch(i){
            case 0: 
                Destroy(food);
                food = Spawner(Food, 0);
            break;
            case 1:
                Destroy(superFood);
            break;
            case 2:
                Destroy(buffSpeedUp);
            break;
            case 3:
                Destroy(buffSpeedDown);
            break;
        }
    }
    private Vector2 GetRandomPosition(Bounds bounds){
        Vector2 position = new Vector2(Random.Range(bounds.min.x, bounds.max.x),Random.Range(bounds.min.y, bounds.max.y));
        return position;
    }
    private bool IsInsideSnake(Vector2 position){
        List<Transform> snake = this.snakeInstance._segments;

        for (int i = 0; i < snake.Count; i++){
            if ((position.x == snake[i].position.x) && (position.y == snake[i].position.y)){
                return true;
            }
        }

        for (int i = 0; i < didItSpawnArleady.Count; i++){
            if(didItSpawnArleady[i]){
                if ((position.x == objectPosition[i].x) && (position.y == objectPosition[i].y)){
                    return true;
                }
            }
        }
        return false;
    }
    public void ResetInstantitatedObjects(){
        Destroyer(0);
        if (superFood){
            Destroyer(1);
        }
        if (buffSpeedUp){
            Destroyer(2);
        }
        if (buffSpeedDown){
            Destroyer(3);
        }
        randomTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
    /*
    
        if (randomTime <= 0){
            if (start == true) {
                SpawnSuperFood();
                start = false;
            } else if ((start == false) && (!superFood)){
                SpawnSuperFood();
            } else if ((randomTime <= -maxSuperFoodLastingTime)&&(superFood)){
                Destroy(superFood);
                randomTime = Random.Range(minSuperFoodSpawnTime, maxSuperFoodSpawnTime);
            } else if (!superFood){
                randomTime = Random.Range(minSuperFoodSpawnTime, maxSuperFoodSpawnTime);
            }
        }
    
    
    private void SpawnFood(){
        Bounds bounds = this.gridArea.bounds;
        objectPositions[0] = GetRandomPosition(bounds);
        while(IsInsideSnake(objectPositions[0], 1)){
            objectPositions[0] = GetRandomPosition(bounds);
        }
        food = Instantiate(Food, new Vector3(Mathf.Round(objectPositions[0].x), Mathf.Round(objectPositions[0].y), 0.0f), Quaternion.identity);
    }
    private void SpawnSuperFood(){
        Bounds bounds = this.gridArea.bounds;
        superFoodPosition = GetRandomPosition(bounds);
        while(IsInsideSnake(superFoodPosition, 2)){
            superFoodPosition = GetRandomPosition(bounds);
        }
        superFood = Instantiate(SuperFood, new Vector3(Mathf.Round(superFoodPosition.x), Mathf.Round(superFoodPosition.y), 0.0f), Quaternion.identity);
    }
    
    IsInsideSnake -- before
    for (int i = 0; i < snake.Count; i++)
        {
            switch(check){
                case 1:
                    if ((position.x == snake[i].position.x) && (position.y == snake[i].position.y)){
                        if (superFood){
                            if ((position.x == superFoodPosition.x) && (position.y == superFoodPosition.y)){
                                return true;
                            }
                            return true;
                        }
                        return true;
                    }
                    break;
                case 2:
                    if ((position.x == snake[i].position.x) && (position.y == snake[i].position.y) && (position.x == objectPositions[0].x) && (position.y == objectPositions[0].y)){
                        return true;
                    }
                    break;
            }   
        }
        return false;
        
    public void DestroyInstantiatedFood(){
        Destroy(food);
        food = Spawner(Food, 0);
    }
    public void DestroyInstantiatedSuperFood(){
        Destroy(superFood);
        randomTime = Random.Range(minSuperFoodSpawnTime, maxSuperFoodSpawnTime);
    }*/