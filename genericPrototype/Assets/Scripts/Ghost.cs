using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

        // Check if there is a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
                Debug.Log("Touched the UI");
            }
        }

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
        Handheld.Vibrate();
		hasAttacked = true;
		doubleTap++;
	}

	public void resetTap(){
		doubleTap = INIT_DOUBLETAP_VALUE;
	}
}
