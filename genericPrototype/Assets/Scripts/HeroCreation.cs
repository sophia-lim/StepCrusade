using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroCreation : MonoBehaviour {
    
    public InputField heroNameInput;
    public Text heroName;

    public GameObject congratulationMessagePopup;
    public GameObject characterModel;

    private int activeInventoryIndex;
    private int selectedInventoryIndex;
    
    public Texture[] skinTextures = new Texture[4];

    // Use this for initialization
    void Start () {
        congratulationMessagePopup.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        SaveManager.Instance.state.username = heroName.text;
    }

    public void changeSkinColour_01() {
        //SaveManager.Instance.state.currentSkin = 1;
        SaveManager.Instance.state.activeEquipment = 0;
        Manager.Instance.playerMaterial.mainTexture = Manager.Instance.playerTextures[0];
        Manager.Instance.skinIndex = 0;
        SaveManager.Instance.Save();
    }

    public void changeSkinColour_02() {
        //SaveManager.Instance.state.currentSkin = 2;
        SaveManager.Instance.state.activeEquipment = 1;
        Manager.Instance.playerMaterial.mainTexture = Manager.Instance.playerTextures[1];
        Manager.Instance.skinIndex = 1;
        SaveManager.Instance.Save();
    }

    public void changeSkinColour_03() {
        //SaveManager.Instance.state.currentSkin = 3;
        SaveManager.Instance.state.activeEquipment = 2;
        Manager.Instance.playerMaterial.mainTexture = Manager.Instance.playerTextures[2];
        Manager.Instance.skinIndex = 2;
        SaveManager.Instance.Save();
    }

    public void changeSkinColour_04() {
        //SaveManager.Instance.state.currentSkin = 4;
        SaveManager.Instance.state.activeEquipment = 3;
        Manager.Instance.playerMaterial.mainTexture = Manager.Instance.playerTextures[3];
        Manager.Instance.skinIndex = 3;
        SaveManager.Instance.Save();
    }


    // Pops up a congratulations dialog box once the user applies the skin and eyes
    public void popUp() {
        congratulationMessagePopup.SetActive(true);
        saveCharacter();
    }
    
    //Save state and register
    private void saveCharacter() {
        congratulationMessagePopup.SetActive(false);
        SaveManager.Instance.state.username = heroName.text;
        Debug.Log(heroName.text);
        Debug.Log(SaveManager.Instance.state.username);
        SaveManager.Instance.state.hasRegistered = true;
        SaveManager.Instance.Save();
        congratulationMessagePopup.SetActive(true);
    }

    public void goToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
