using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour {

    Image fillImg;
    public GameObject progressBar;
 

    // Use this for initialization
    void Start() {
        fillImg = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (progressBar.tag == "dailyGoal") {
            fillImg.fillAmount = SaveManager.Instance.state.dailyStepCounts / SaveManager.Instance.state.maxDailySteps;
        } else if (progressBar.tag == "experience") {
            fillImg.fillAmount = SaveManager.Instance.state.dailyStepCounts / SaveManager.Instance.state.maxLevelSteps;
        }
    }

}
