using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeedUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player") {
            SpawnManager.instance.Destroyer(2);
        }
    }
}
