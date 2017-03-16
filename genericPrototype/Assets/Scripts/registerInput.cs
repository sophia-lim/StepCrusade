using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class registerInput : MonoBehaviour {

    private InputField mainInputField;
    private string textInput;

    // Checks if there is anything entered into the input field.
    // From Unity3D documentation: https://docs.unity3d.com/ScriptReference/UI.InputField-onEndEdit.html
    void LockInput(InputField input) {
        if (input.text.Length > 0) {
            Debug.Log("Text has been entered");
            Debug.Log(input.text);
        } else if (input.text.Length == 0) {
            Debug.Log("Main Input Empty");
        }
    }

    // Use this for initialization
    void Start () {
        mainInputField.onEndEdit.AddListener(delegate { LockInput(mainInputField); });
    }

    //Setter for InputField
    public void setInput(InputField mainInputField) {
        this.mainInputField = mainInputField;
    }

    //Getter for InputField
    public InputField getInput() {
        return mainInputField;
    }

    //Setter for text Input
    public void setTextInput(string textInput){
        this.textInput = textInput;
    }

    //Getter for text Input
    public string getTextInput() {
        return textInput;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
