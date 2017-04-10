using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MotionPotion : MonoBehaviour {

    public bool isUsed;
    public int timeLeft = 30;
    public Text countdownText;

    //Have the counter exist throughout scenes
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
        isUsed = false;
	}
	
	// Update is called once per frame
	void Update () {
        countdownText.text = ("Time left: " + timeLeft);
        if (timeLeft <= 0) {
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";
        }
	}

    //When motion potion is clicked, begin countdown
    public void useMotionPotion() {
        //remove one instance of motion potion
        StartCoroutine("LoseTime");
    }

    //Wait for a second, than decrement time left
    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    //Test onClick function to move onto new scene
    public void loadNewScene() {
        SceneManager.LoadScene("powerUpsTestScene2");
    }

}
