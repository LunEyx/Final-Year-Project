using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle"){
            Destroy(gameObject);
        }
    }
}
