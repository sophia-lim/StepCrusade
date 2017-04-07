using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Default action code
public class ActionItem : Interactable {

	//Cant override action
	public override void Interact(){
		Debug.Log ("Interacting with base action object.");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
