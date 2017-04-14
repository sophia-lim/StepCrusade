using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	// how fast the player moves.
	public float speed = 5;

	// where we want to travel to.
	private Vector3 targetPosition;

	// toggle to check if we are moving or not.
	public bool isMoving;

	//public bool isKilling;

	const int LEFT_MOUSE_BUTTON = 0;

	// Use this for initialization
	void Start () 
	{
		// set the target position to where we are at the start.
		targetPosition = transform.position;
	
		isMoving = false;

		//isKilling = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON))
		{
			SetTargetPosition ();
		}

		if(isMoving)
		{
			MovePlayer ();
		}

		/*if (isKilling) 
		{
			onMouseDown ();
		}*/
	}

	void SetTargetPosition()
	{
		Plane plane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float point = 0f;

		if (plane.Raycast (ray, out point))
			targetPosition = ray.GetPoint (point);

		//We begin to move
		isMoving = true;
	}

	void MovePlayer()
	{
		transform.LookAt (targetPosition);
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

		if (transform.position == targetPosition) 
		{
			isMoving = false;
			Debug.DrawLine (transform.position, targetPosition, Color.red);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		isMoving = false;
		speed = 0;
		//isKilling = true;

	}

	/*void onMouseDown()
	{
		
		Destroy (GameObject.FindGameObjectWithTag ("Enemy"));
	}*/
}