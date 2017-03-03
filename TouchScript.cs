using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		//Write data for each touch input
		foreach (Touch inputTouch in Input.touches) {
			string message = "";
			message += "ID: " + inputTouch.fingerId + "\n";
			message += "Phase: " + inputTouch.phase.ToString() + "\n";
			message += "TapCount: " + inputTouch.tapCount + "\n";
			message += "Pos X: " + inputTouch.position.x + "\n";
			message += "Pos Y: " + inputTouch.position.y + "\n";

			//Offset finger input all across the screen
			int num = inputTouch.fingerId;
			GUI.Label(new Rect(0 + 130 * num, 0, 120, 100);
		}
	}
}
