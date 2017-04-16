using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterInput : MonoBehaviour {
    

    // Checks if there is anything entered into the input field.
    // From Unity3D documentation: https://docs.unity3d.com/ScriptReference/UI.InputField-onEndEdit.html

    public InputField emailInput;
    public Text email;

    public InputField passwordInput;
    private Text password;

    // Checks if there is anything entered into the input field.
    void LockInput(InputField input, Text textObj) {
        if (input.text.Length > 0) {
            Debug.Log("Text has been entered");
            textObj.text = input.text;
            Debug.Log(input.text);
        } else if (input.text.Length == 0) {
            Debug.Log("Main Input Empty");
        }
    }

    public void Start() {
        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        emailInput.onEndEdit.AddListener(delegate { LockInput(emailInput, email); });
        passwordInput.onEndEdit.AddListener(delegate { LockInput(passwordInput, password); });
    }


    // Update is called once per frame
    void Update() {
    }



    /*********************
    ***** NAVIGATION *****
    **********************/

    public void goToLoginScreen() {
        SceneManager.LoadScene("Login");
    }

    public void goToMenuScreen() {
        SceneManager.LoadScene("Menu");
    }
}