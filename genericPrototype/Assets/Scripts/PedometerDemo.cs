﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PedometerDemo : MonoBehaviour {
	
	private PedometerPlugin pedometerPlugin;
	private string demoName="[PedometerDemo] ";
	private UtilsPlugin utilsPlugin;

	public Text hasStepDetectorStatusText;
	public Text stepCountText;
	public Text stepDetectText;

	//Edit by StepCrusade for mana crusade
	public Text manaCrusadeText;
	public Text stepCountCrusadeText;
    public int stepCountCrusade = 0;
    public int dailyStepCounts = 0;
	public bool isTapped = false;

	//Edit by StepCrusade for level
	public Text levelCrusadeText;
	public int levelCrusade = 0;

	//Go back to menu
    public void goToMenu() {
        SceneManager.LoadScene("Menu");
    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

	
	// Use this for initialization
	void Start (){
		//get the instance of pedometer plugin
		pedometerPlugin = PedometerPlugin.GetInstance();

		//set to zero to hide debug toast messages
		pedometerPlugin.SetDebug(0);

		utilsPlugin = UtilsPlugin.GetInstance();
		utilsPlugin.SetDebug(0);

		//check if step detector is supported
		bool hasStepDetector = utilsPlugin.HasStepDetector();

		if(hasStepDetector){
			UpdateStepDetectorStatus("available");

			//initialze pedometer
			pedometerPlugin.Init();
			
			//set this to true to always starts at zero steps, else set to false to continue steps
			pedometerPlugin.SetAlwaysStartAtZero(true);
			
			//set call back listener for pedometer events
			pedometerPlugin.SetCallbackListener(OnStepCount,OnStepDetect);
			
			//register sensor event listener and pass sensor delay that you want
			pedometerPlugin.RegisterSensorListener(SensorDelay.SENSOR_DELAY_FASTEST);

		} else {
			UpdateStepDetectorStatus("not available");
		}


        if (SaveManager.Instance.state.motionPotionCount > 0) {
            useMotionPotion();
        }
    }

	void Update() {
		manaCrusadeText.text = SaveManager.Instance.state.manaCrusade.ToString ();
		stepCountCrusadeText.text = SaveManager.Instance.state.dailyStepCounts.ToString ();
        if (SaveManager.Instance.state.motionPotionCountdown <= 0) {
            SaveManager.Instance.state.motionPotionCount = 0;
            StopCoroutine("LoseTime");
            SaveManager.Instance.Save();
        }
    }

	public void ResetTotalStep(){
		//reset total step to zero
		if(pedometerPlugin!=null){
			pedometerPlugin.ResetTotalStep();
			UpdateStepCount(0);
			Debug.Log( demoName + "ResetTotalStep ");
		}
	}	
				 
	private void OnApplicationPause(bool val){
		if(val){
			//game is pause
			//remove sensor event listener
			if(pedometerPlugin!=null){
				pedometerPlugin.RemoveSensorListener();
			}
		}else{
			//game is resume
			//register sensor event listener
			if(pedometerPlugin!=null){
				pedometerPlugin.ResetTotalStep();
				//register sensor event listener and pass sensor delay that you want
				pedometerPlugin.RegisterSensorListener(SensorDelay.SENSOR_DELAY_FASTEST);
			}
		}
	}

	//step count event is triggered
	private void OnStepCount(int count){
		UpdateStepCount(count);
		Debug.Log( demoName + "OnStepCount count " + count);
	}

	//step detect event is triggered
	private void OnStepDetect(){
		UpdateStepDetect("Detect!");
		Debug.Log( demoName + "OnStepDetect");
	}

	private void UpdateStepDetectorStatus(string val){
		if(hasStepDetectorStatusText!=null){
			hasStepDetectorStatusText.text = String.Format("Status: {0}", val);
		}
	}

	//for updating step count for demo purpose
	private void UpdateStepCount(int count){
		if(stepCountText!=null){
			stepCountText.text = String.Format("Step Count: {0}",count);
			stepCountCrusade = count - (SaveManager.Instance.state.manaCrusade *200);
			calculateMana (stepCountCrusade);
			calculateLevelCrusade (count);
		}
	}

	//for showing step is detected for demo purpose
	private void UpdateStepDetect(string val){
		if(stepDetectText!=null){
			stepDetectText.text = String.Format("Step Detect: {0}",val);
		}
	}

	//Transforms step counts into mana (activity level)
    //For testing purposes, the value is 10, but in-game should be 200
	public void calculateMana(int stepCrusadeCounts){
        //If there's 1 motion potion, use it to increase X5
        if (stepCrusadeCounts != 0 && stepCrusadeCounts % 10 == 0 && SaveManager.Instance.state.manaCrusade != 100 && SaveManager.Instance.state.motionPotionCountdown > 0) {
            SaveManager.Instance.state.manaCrusade += 5;
        //Else just increment normally by 1
        } else if (stepCrusadeCounts != 0 && stepCrusadeCounts%10 == 0 && SaveManager.Instance.state.manaCrusade != 100) {
            SaveManager.Instance.state.manaCrusade += 1;
			updateStepCrusadeCounts();
		}
	}

	//Updates mana after hitting ghosts
	public void updateMana() {
		if (isTapped && SaveManager.Instance.state.manaCrusade > 0) {
            SaveManager.Instance.state.manaCrusade -= 1;
		} else if (isTapped && SaveManager.Instance.state.manaCrusade == 0){
			Debug.Log ("Not enough mana.");
		}
	}

	//Subtract step counts w/ 1000 after count
	public void updateStepCrusadeCounts(){
		stepCountCrusade -= 10;
	}

    //Use motion potion when there is one
    private void useMotionPotion() {
        //remove one instance of motion potion
        if (SaveManager.Instance.state.motionPotionCount > 0 || SaveManager.Instance.state.motionPotionCountdown == 0) {
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

    //Leveling up 
    public void calculateLevelCrusade(int stepCounts) {
		if (stepCounts != 0 && stepCounts % 50 == 0 && levelCrusade <= 99) {
			levelCrusade += 1;
			levelCrusadeText.text = levelCrusade.ToString();
		}
	}

}