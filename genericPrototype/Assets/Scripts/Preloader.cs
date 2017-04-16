using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour 
{
	// The CanvasGroup created on the gameObject.
	private CanvasGroup fadeGroup;

	// To keep track of time.
	private float loadTime;


	// The minimum time of that scene. Will show the logo for a minimum of 3 seconds.
	private float minimumLogoTime = 3.0f; 

	private void Start() 
	{
        Debug.Log(System.DateTime.Now);
        // If it is the first time opening the game, save the date
        // Else, if it not the first time, check if the loading date has changed
        // --- If it has changed by over a day, reset the daily step counts
        // --- If it has not changed, do not reset dailyr step counts
        if (SaveManager.Instance.state.firstLoading) {
            Debug.Log("First time loading game: " + System.DateTime.Now.Day);
            SaveManager.Instance.state.firstLoadingDate = System.DateTime.Now.Day;
            SaveManager.Instance.state.lastDay = System.DateTime.Now.Day;
            SaveManager.Instance.state.firstLoading = false;
            SaveManager.Instance.Save();
        } else {
            if (System.DateTime.Now.Day - SaveManager.Instance.state.lastDay >= 1) {
                //Reset step count
                SaveManager.Instance.state.dailyStepCounts = 0;
            }
        }

		// Grab the only Canvas Group in the scene.
		// If there are multiple canvas groups in the scene, this will return the wrong thing and not work.
		fadeGroup = FindObjectOfType<CanvasGroup>();

		//Fade is full opactity. 
		fadeGroup.alpha = 1;

		//Pre load the game.

		//Get a timestamp of the completion time.
		// if loadtime is super fast, give it a small buffer time so we can appreciate Roxanne's logo!
		if (Time.time < minimumLogoTime)
			loadTime = minimumLogoTime;
		else
			loadTime = Time.time;
	}

	private void Update() 
	{
		// Fade In.
		if(Time.time < minimumLogoTime) 
		{
			fadeGroup.alpha = 1 - Time.time;
		}

		// Fade Out.
		if (Time.time > minimumLogoTime && loadTime != 0) 
		{
			fadeGroup.alpha = Time.time - minimumLogoTime;
			if (fadeGroup.alpha >= 1) 
			{
                if (SaveManager.Instance.state.hasRegistered == false) {
                    SceneManager.LoadScene("UserAuthentication");
                } else if (SaveManager.Instance.state.hasRegistered == true) {
                    SceneManager.LoadScene("Menu");
                }
			}
		}
	}


}
