using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFood : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player") {
            SpawnManager.instance.Destroyer(1);
            ScoreManager.instance.AddPoints(5);
        }
    }
}
