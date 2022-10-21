using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
	
	// Rigidbody of Ball
    public Rigidbody2D rb;

	// Get position of Hook
	private Vector2 hookPos;

	public float releaseTime = .25f;
	public float maxDragDistance = 1.5f;
	public float Force = 15f;

	public GameObject nextBall;

	private bool isPressed = false;
	public GameObject PointPrefab;
	private GameObject[] Points;
	public int numberOfPoints;
	private Vector2 Direction;
	public bool shoot = false;
	public float timeUpdate;
	public Vector2 rootPos;
	
	void Start(){
		// Create list of prediction location of BALL
		Points = new GameObject[numberOfPoints];
		for(int i = 0; i<numberOfPoints; i++){
			Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
		}
		GameObject hook = GameObject.FindGameObjectWithTag("hook");
		hookPos = (Vector2)hook.transform.position;
	}

	void Update ()
	{
		// Drag Ball -> Update Ball position when Drag, but limit distance drag 
		if (isPressed)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Limit distance drag
			if (Vector3.Distance(mousePos, hookPos) > maxDragDistance)
				rb.position = hookPos + (mousePos - hookPos).normalized * maxDragDistance;
			else
				rb.position = mousePos;

			// Predict shoot movement
			for (int i = 0; i < Points.Length; i++){
				Points[i].transform.position = PointPosition(i * 0.1f);
			}
			// Get position and direction of BALL when drag
			Vector2 heading = hookPos - (Vector2)transform.position;
			float distance = heading.magnitude;
			Direction = heading/distance;
			rootPos = transform.position;
		}
		// When shoot, Update shoot movement of Ball by formula
		if(shoot){
			timeUpdate += Time.deltaTime;
			gameObject.transform.position = (Vector2)rootPos + ((Direction).normalized * Force * timeUpdate * (hookPos - (Vector2)rootPos).magnitude + 0.5f*Physics2D.gravity *(timeUpdate * timeUpdate));
		}

	}

	void OnMouseDown ()
	{
		isPressed = true;
		// GetComponent<SpringJoint2D>().enabled = false;

		// Remove Drag force when mouse down
		rb.isKinematic = true;
	}

	void OnMouseUp ()
	{
		shoot = true;
		isPressed = false;
		// GetComponent<SpringJoint2D>().enabled = false;
		// Option 1: Update shoot movement by Velocity
		// gameObject.GetComponent<Rigidbody2D>().velocity = (Direction) * (hook.position - (Vector2)transform.position).magnitude * Force;
		rb.isKinematic = false;
		StartCoroutine(Release());
	}

	IEnumerator Release()
	{

		for (int i = 0; i < Points.Length; i++){
			Destroy(Points[i]);
		}
		yield return new WaitForSeconds(releaseTime);

		// GetComponent<SpringJoint2D>().enabled = false;
		// this.enabled = false;

		yield return new WaitForSeconds(releaseTime);
        nextBall.SetActive(true);
		
	}

	void OnCollisionEnter2D(Collision2D other){
		shoot = false;
	}
	Vector2 PointPosition(float t){
		Vector2 currentPointPos = (Vector2)transform.position + ((Direction).normalized * Force * t	)* (hookPos - (Vector2)transform.position).magnitude + 0.5f*Physics2D.gravity *(t*t);
		return currentPointPos;
	}
}
