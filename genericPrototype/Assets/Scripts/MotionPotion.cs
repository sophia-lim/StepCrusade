using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MotionPotion : MonoBehaviour {

    public bool isUsed;
    public int timeLeft = 30;
    public Text countdownText;

    public int costMotionPotion = 10;
    public int testGold = 10;
    public Text motionPotionCountText;

    //Have the counter exist throughout scenes
    void Awake() {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
        isUsed = false;
        SaveManager.Instance.state.motionPotionCountdown = 30;
        SaveManager.Instance.state.motionPotionCount = 0;

        motionPotionCountText.text = SaveManager.Instance.state.motionPotionCount.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        countdownText.text = ("Time left: " + timeLeft);
        if (timeLeft <= 0) {
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";
        }

        motionPotionCountText.text = SaveManager.Instance.state.motionPotionCount.ToString();
	}

    public void buyMotionPotion() {
        if (costMotionPotion > testGold) {
            Debug.Log("Cannot purchase motion potion.");
        } else if (costMotionPotion == testGold || costMotionPotion < testGold) {
            Debug.Log("Purchased motion potion.");
            SaveManager.Instance.state.motionPotionCount++;
            testGold -= costMotionPotion;
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
