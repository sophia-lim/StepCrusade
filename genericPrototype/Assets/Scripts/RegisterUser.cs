using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterUser : MonoBehaviour {


    private Vector3 panelPosition;
    private int currentPanel = 0;

    public RectTransform mainPanel;

    // Use this for initialization
    void Start() {
        Debug.Log("Character has loaded.");
    }

    // Update is called once per frame
    void Update() {
        // If you want the speed of the transition to be faster increase 0.1f, to slow it, decrease 0.1f
        mainPanel.anchoredPosition3D = Vector3.Lerp(mainPanel.anchoredPosition3D, panelPosition, 0.1f);
    }

    /***************************
    ** METHODS FOR NAVIGATION **
    ***************************/

    //Checks current panel and which panel to go, move to panel, and update current panel index
    private void changePanelPosition(int panelIndex) {
        Debug.Log("The current panel is:" + currentPanel);

        if (currentPanel == 0 && panelIndex == 1) {
            Debug.Log("Going to Register");
            panelPosition = Vector3.left * 1280;
        } else if (currentPanel == 1 && panelIndex == 0) {
            Debug.Log("Going to Create");
            panelPosition = Vector3.right * 0;
        } else if (currentPanel == 1 && panelIndex == 2) {
            Debug.Log("Going to Confirm");
            panelPosition = Vector3.left * 2560;
        } else if (currentPanel == 2 && panelIndex == 1) {
            Debug.Log("Going to Register 2");
            panelPosition = Vector3.left * 1280;
        }

        currentPanel = panelIndex;
        Debug.Log("The updated panel is:" + currentPanel);
    }

    //Move to createHeroPanel
    public void goToPanelCreate() {
        changePanelPosition(0);
    }

    //Move to createUserPanel
    public void goToPanelRegister() {
        Debug.Log("Create hero button clicked.");
        changePanelPosition(1);
    }

    //Move to createConfirmationPanel
    public void goToPanelConfirmation() {
        changePanelPosition(2);
    }

    //Move back to menu
    public void goToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
