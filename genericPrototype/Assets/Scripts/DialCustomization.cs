using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialCustomization : MonoBehaviour {

	public Button dialCustomizationShop;

	void Start() 
	{
		Button btn = dialCustomizationShop.GetComponent<Button>();
		btn.onClick.AddListener(goToDialCustomizationScene);
	}

	public void goToDialCustomizationScene ()
	{
		SceneManager.LoadScene ("Shop_Customization");
	}

}
