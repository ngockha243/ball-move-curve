using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
	public Rigidbody2D hook;

	public float releaseTime = .15f;
	public float maxDragDistance = 1.5f;
	public float Force = 15f;

	public GameObject nextBall;

	private bool isPressed = false;
	public GameObject PointPrefab;
	public GameObject[] Points;
	public int numberOfPoints;
	Vector2 Direction;
	
	void Start(){
		Points = new GameObject[numberOfPoints];
		for(int i = 0; i<numberOfPoints; i++){
			Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
		}
	}

	void Update ()
	{
		if (isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			else
				rb.position = mousePos;

			for (int i = 0; i < Points.Length; i++){
				Points[i].transform.position = PointPosition(i * 0.1f);
			}
		}

		Vector2 heading = hook.position - (Vector2)transform.position;
		float distance = heading.magnitude;
		Direction = heading/distance;
	}

	void OnMouseDown ()
	{
		isPressed = true;
		GetComponent<SpringJoint2D>().enabled = false;
		// rb.isKinematic = true;
		
	}

	void OnMouseUp ()
	{
		isPressed = false;
		GetComponent<SpringJoint2D>().enabled = false;
		gameObject.GetComponent<Rigidbody2D>().velocity = (Direction) * (hook.position - (Vector2)transform.position) * Force;
		// rb.isKinematic = false;
		StartCoroutine(Release());
	}

	IEnumerator Release ()
	{

		for (int i = 0; i < Points.Length; i++){
			Destroy(Points[i]);
		}
		yield return new WaitForSeconds(releaseTime);

		// GetComponent<SpringJoint2D>().enabled = false;
		
		this.enabled = false;

		yield return new WaitForSeconds(.25f);
        nextBall.SetActive(true);
		
	}

	Vector2 PointPosition(float t){
		Vector2 currentPointPos = (Vector2)transform.position + ((Direction).normalized * Force * t	)* (hook.position - (Vector2)transform.position) + 0.5f*Physics2D.gravity *(t*t);
		return currentPointPos;
	}
}
