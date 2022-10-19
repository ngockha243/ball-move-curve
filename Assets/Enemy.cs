using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject obj;
    // public GameObject enemy;
    void Start()
    {
        obj = GameObject.Find("GameObject");
        obj.GetComponent<gameController>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "shoot"){
            obj.GetComponent<gameController>().score += 10;
            obj.GetComponent<gameController>().isEnemyActive = false;
            Destroy(this);
            Destroy(other.collider.gameObject);
        }
    }
}
