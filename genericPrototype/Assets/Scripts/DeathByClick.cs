using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeathByClick : MonoBehaviour {

    //Exported from Ghost class script

    public Text ghostHPText;
    public int ghostHP;

    public int doubleTap;
    public Text doubleTapText;

    public bool isDead;

    public readonly int INIT_DOUBLETAP_VALUE = 0;
    public readonly int INIT_GHOSTHP_VALUE = 10;

    // Initialize HP
    void Start() {
        ghostHP = INIT_GHOSTHP_VALUE;
        doubleTap = INIT_DOUBLETAP_VALUE;
        isDead = false;
    }

    // Update the ghost HP as well as the taps
    void Update() {
        //Cast int to UI
        ghostHPText.text = ghostHP.ToString();
        doubleTapText.text = doubleTap.ToString();
    }

    // Updated by Will: Commit 4/12/2017
    // When clicking ghost, decrease HP
    void OnMouseDown() {
        ghostHP -= 1;
        Handheld.Vibrate();

        if (ghostHP == 0) {
			
            // Before destroying the gameObject, allow character to move again
            Debug.Log("Setting killed monster to true");
            SaveManager.Instance.state.gold += Random.Range(0, 10);
            SaveManager.Instance.Save();
            PlayerMovement.instance.killedMonster = true;

			Destroy(gameObject);
        }
    }
}