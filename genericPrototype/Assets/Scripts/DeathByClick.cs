using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeathByClick : MonoBehaviour {
	//public int health;

	public Text ghostHPText;
	public int ghostHP;

	public int doubleTap;
	public Text doubleTapText;

	public readonly int INIT_DOUBLETAP_VALUE = 0;
	public readonly int INIT_GHOSTHP_VALUE = 3;

	void Start () 
	{
			ghostHP = INIT_GHOSTHP_VALUE;
			doubleTap = INIT_DOUBLETAP_VALUE;
		
	}

	void Update () 
	{
		//Cast int to UI
		ghostHPText.text = ghostHP.ToString ();
		doubleTapText.text = doubleTap.ToString ();
	}

	void OnMouseDown () 
	{
		ghostHP -= 1;
		Handheld.Vibrate();

		if (ghostHP == 0)
		{
			Destroy (gameObject);	
		}
	}
}
