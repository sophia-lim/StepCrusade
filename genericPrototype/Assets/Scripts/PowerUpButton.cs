using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUpButton : MonoBehaviour {

	public Button powerUpShop;

	void Start() 
	{
		Button btn = powerUpShop.GetComponent<Button>();
		btn.onClick.AddListener(goToPowerUpScene);
	}

	public void goToPowerUpScene ()
	{
		SceneManager.LoadScene ("Shop_PowerUp");
	}

}
