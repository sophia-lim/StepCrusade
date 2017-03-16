using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public static Manager Instance {set; get;}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	// Used when changing from menu to game scene.
	public int currentLevel = 0;
	// Used when going from entering menu scene from game scene.
	public int menuFocus = 0;
}
