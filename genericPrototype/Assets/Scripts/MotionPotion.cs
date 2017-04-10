using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotionPotion : MonoBehaviour {

    public bool isUsed;
    public int timeLeft = 5;
    public Text countdownText;

   // void Awake() {
   //     DontDestroyOnLoad(this);
   // }

	// Use this for initialization
	void Start () {
        isUsed = false;
        StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        countdownText.text = ("Time left: " + timeLeft);
        if (timeLeft <= 0) {
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";
        }
	}

    public void useMotionPotion() {
        //remove one instance of motion potion
        isUsed = true;
    }

    private void motionPotionTimer() {
        if (isUsed) {
            //Begin counter
        }
    }

    //Wait for a second, than decrement time left
    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
