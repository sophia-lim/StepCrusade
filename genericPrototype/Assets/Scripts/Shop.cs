using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

    public Text gold;

	// Use this for initialization
	void Start () {
        gold.text = SaveManager.Instance.state.gold.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        gold.text = SaveManager.Instance.state.gold.ToString();
    }


    public void goToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
