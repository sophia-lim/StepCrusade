﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour 
{

	private CanvasGroup fadeGroup;
	private float fadeInDuration = 2;
	private bool gameStarted;

	private void Start()
	{
		// Get the only canvas group in the scene.
		fadeGroup = FindObjectOfType<CanvasGroup>();
		//Set the fade to full opacity.
		fadeGroup.alpha = 1;
	}

	private void Update()
	{
		if(Time.timeSinceLevelLoad <=fadeInDuration)
		{
			//Initial fade in.
			fadeGroup.alpha = 1 - (Time.timeSinceLevelLoad / fadeInDuration);
		}
		//if the Initial fade in is completed, and the game has not been started yet.
		else if (!gameStarted)
		{	
			//Ensure fade is completely gone.
			fadeGroup.alpha = 0;
			gameStarted = true;
		}
	}

	public void CompleteLevel()
	{
		//Complete the level & save the progress
		SaveManager.Instance.CompleteLevel(Manager.Instance.currentLevel);

		// Focus the level selection when we return to the menu scene.
		Manager.Instance.menuFocus = 2;

		ExitScene();
	}

	public void ExitScene()
	{
		SceneManager.LoadScene("Menu");
	}

	
}
