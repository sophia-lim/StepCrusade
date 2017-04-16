using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MotionPotion : MonoBehaviour {

    //public Text countdownText;
    // Motion potion only acts for 5 minutes (300 seconds)
    private int motionPotionLifespan = 300;

    public int costMotionPotion = 50;
    //public int testGold = 10;
    //public Text motionPotionCountText;

	// Use this for initialization
	void Start () {
        //motionPotionCountText.text = SaveManager.Instance.state.motionPotionCount.ToString();
        if(SaveManager.Instance.state.motionPotionCountdown > 0) {
            StartCoroutine("LoseTime");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //countdownText.text = ("Time left: " + SaveManager.Instance.state.motionPotionCountdown);
        if (SaveManager.Instance.state.motionPotionCountdown <= 0) {
            StopCoroutine("LoseTime");
            //countdownText.text = "Times Up!";
        }
        //motionPotionCountText.text = SaveManager.Instance.state.motionPotionCount.ToString();
	}

    public void buyMotionPotion() {
        if (costMotionPotion > SaveManager.Instance.state.gold) {
            Debug.Log("Cannot purchase motion potion.");
        } else if (costMotionPotion <= SaveManager.Instance.state.gold) {
            Debug.Log("Purchased motion potion.");
            SaveManager.Instance.state.motionPotionCount++;
            SaveManager.Instance.state.gold -= costMotionPotion;
            SaveManager.Instance.Save();
        }
    }

    /*When motion potion is clicked, begin countdown
    public void useMotionPotion() {
        //remove one instance of motion potion
        if(SaveManager.Instance.state.motionPotionCount > 0 || SaveManager.Instance.state.motionPotionCountdown == 0) {
            SaveManager.Instance.state.motionPotionCount--;
            SaveManager.Instance.state.motionPotionCountdown = 300;
            SaveManager.Instance.Save();
            StartCoroutine("LoseTime");
        }
    }

    //Wait for a second, than decrement time left
    IEnumerator LoseTime() {
        while (true) {
            yield return new WaitForSeconds(1);
            SaveManager.Instance.state.motionPotionCountdown--;
            SaveManager.Instance.Save();
        }
    }

    //Test onClick function to move onto new scene
    public void loadNewScene() {
        SceneManager.LoadScene("powerUpsTestScene2");
    }*/

}
