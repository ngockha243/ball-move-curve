using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public GameObject ballDeathEffect;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "shoot"){
            gameController.score += 10;
            gameController.isEnemyActive = false;
            Die();
            Destroy(other.collider.gameObject);
            Instantiate(ballDeathEffect, other.gameObject.transform.position, Quaternion.identity);
        }
    }

    void Die(){
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
