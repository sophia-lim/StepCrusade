﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour {

	public Button ghostInstance;
	public Text ghostHPText;
	public int ghostHP;
	private bool hasAttacked = false;
	public int doubleTap;
	public Text doubleTapText;

	public readonly int INIT_DOUBLETAP_VALUE = 0;
	public readonly int INIT_GHOSTHP_VALUE = 3;


	// Use this for initialization
	void Start () {
		ghostHP = INIT_GHOSTHP_VALUE;
		doubleTap = INIT_DOUBLETAP_VALUE;
	}
	
	// Update is called once per frame
	void Update () {
		//Cast int to UI
		ghostHPText.text = ghostHP.ToString ();
		doubleTapText.text = doubleTap.ToString ();

		//If player is attacking and has tapped twice
		if (hasAttacked && doubleTap == 2) {
			ghostHP--;
			resetTap ();
			hasAttacked = false;
		} else if (hasAttacked) {
			hasAttacked = false;
		}

		//If the ghost is out of HP, then destroy its instance
		if (ghostHP == 0) {
			Destroy (gameObject);
		}
	}

	//If player has tapped the ghost
	public void isAttacked(){
		hasAttacked = true;
		doubleTap++;
	}

	public void resetTap(){
		doubleTap = INIT_DOUBLETAP_VALUE;
	}
}