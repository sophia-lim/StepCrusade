using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class goToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Has loaded button.");
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseDown() {
        Debug.Log("Mouse is down");
    }

    //Sophia: Go to menu scene
    public void goToMenuScene() {
        Debug.Log("Has clicked.");
        SceneManager.LoadScene("Menu");
    }
}
