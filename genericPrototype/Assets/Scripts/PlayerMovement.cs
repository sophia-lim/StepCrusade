using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

	// how fast the player moves.
	public float speed;
    const int STOP_MOVEMENT = 0;
    const int CONTINUE_MOVEMENT = 5;

    // where we want to travel to.
    private Vector3 targetPosition;

	// toggle to check if we are moving or not.
	public bool isMoving;

    public bool canKill;

    const int LEFT_MOUSE_BUTTON = 0;

    public bool killedMonster;

	// for the animations
	Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();



        instance = this;
		// set the target position to where we are at the start.
		targetPosition = transform.position;
		isMoving = false;
        killedMonster = false;
        speed = CONTINUE_MOVEMENT;
        canKill = false;
    }
	
	// Update is called once per frame
	void Update () {
		

		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON)){
			SetTargetPosition ();
		}

		anim.SetBool("isWalking", false);
		if(isMoving) {
			
			MovePlayer ();

		}
		anim.SetBool("isSlashing", false);
        if (killedMonster == true) {
            isMoving = true;
            speed = CONTINUE_MOVEMENT;
        }
    }

	void SetTargetPosition() {
		Plane plane = new Plane (Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float point = 0f;

		if (plane.Raycast (ray, out point))
			targetPosition = ray.GetPoint (point);

		//We begin to move
		isMoving = true;
	}

    // Set: rotation of character to click position
    // Move character to clicked position
	void MovePlayer() {
		anim.SetBool("isWalking", true);
		transform.LookAt (targetPosition);
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);

		if (transform.position == targetPosition) {
			isMoving = false;
			Debug.DrawLine (transform.position, targetPosition, Color.red);
		}
	}

    // When the character enters the ghost box collider
    // Set: killedMonster to false
    // Set: movement to zero
    // Set: can kill to true
	void OnTriggerEnter (Collider other) {
		anim.SetBool("isWalking", false);
		anim.SetBool("isSlashing", true);
        killedMonster = false;
        isMoving = false;
        canKill = true;
        speed = STOP_MOVEMENT;
    }
    
}