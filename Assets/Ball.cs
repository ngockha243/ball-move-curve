using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpringJoint2D sj;
    private bool isPressed = false;
    public float releaseTime = 0.15f;
    public bool isShoot = false;
    public Rigidbody2D hook;
    void Update(){
        if(isPressed){
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void Start(){
        // sj.connectedBody(hook);
    }

    void OnMouseDown(){
        isPressed = true;
        rb.isKinematic = true;
    }

    void OnMouseUp(){
        isPressed = false;
        rb.isKinematic = false;
        isShoot = true;
        StartCoroutine(Release());
    }

    IEnumerator Release(){
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
    }
}
