﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendInfo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{	//0 = middle mouse button.
		//1 = right mouse button.
		//2 = left mouse button.
		bool RMB = Input.GetMouseButton (1);

		if (RMB) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			//if(Physics.Raycast(ray, out hit), && hit.transform.tag == "Ground");

				
		}
	}
}
