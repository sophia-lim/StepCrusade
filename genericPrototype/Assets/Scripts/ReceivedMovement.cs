using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivedMovement : MonoBehaviour {

	//The position that the player has clicked that I want them to go to.
	Vector3 newPosition;

	//How fast player travels.
	public float speed;

	//How far the character has to move.
	public float walkRange;


	public GameObject graphics;


	// Use this for initialization
	void Start ()
	{
		//Location where the player currently is in the scene.
		newPosition = this.transform.position;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance (newPosition, this.transform.position) > walkRange)
		{
			this.transform.position = Vector3.MoveTowards (this.transform.position, newPosition, speed * Time.deltaTime);
			Quaternion transRot = Quaternion.LookRotation (newPosition - this.transform.position, Vector3.up);
			graphics.transform.rotation = Quaternion.Slerp (graphics.transform.rotation, transRot, 0.2f);
		}	
	}

	//Receives the place we want to go.
	public void ReceivedMove(Vector3 movePos)
	{
		newPosition = movePos;
	} 
}
