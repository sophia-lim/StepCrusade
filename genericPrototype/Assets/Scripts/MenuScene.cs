using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour 
{
	private CanvasGroup fadeGroup;
	// Last 3 seconds = 0.33f. If u wanted 10 seconds = 0.10f;
	private float fadeInSpeed = 0.33f;
    
	public RectTransform menuContainer; 
	public Transform mapPanel;

	public Transform inventoryPanel;
	public Transform shopPanel;

	public Transform dialPanel;
	public Transform settingsPanel;

	public Text inventoryEquipButton;
	public Text shopPurchaseButton;

    public Text stepsText;
    public Text progressSteps;

    public Text stepsToLvl;
    public Text progressLvl;

    public Text level;
    public Text heroName;
    
	//Setting the price for the actual items in the inventory
	private int[] inventoryCost = new int[] {0, 5, 5, 5, 10, 10, 10, 15, 15, 10 };
	// Setting the proce for the actual items in the shop
	private int[] shopCost = new int[] {0, 20, 40, 40, 60, 60, 80, 85, 85, 100 };
	private int selectedInventoryIndex;
	private int selectedPurchaseIndex; 

	private MenuCamera menuCam;

	public Text goldText;
	private int activeInventoryIndex;
	private int activePurchaseIndex;

	private Vector3 desiredMenuPosition;

	public AnimationCurve enteringLevelZoomCurve;
	private bool isEnteringLevel = false;
	private float zoomDuration = 3.0f;
	private float zoomTransition;

    public GameObject dialogBox;

	private void Start () 
	{
		// Find the only MenuCamera and asign it
		menuCam = FindObjectOfType<MenuCamera>();

		//$$ TEMPORARY....Temporarily setting the amount of gold the player starts with to 999.
		//SaveManager.Instance.state.gold = 100;
        
		// Position our camera on the focu menu.
		SetCameraTo(Manager.Instance.menuFocus);

		//Tell our gold text how much it should be displaying
		UpdateGoldText();

		// Grab the only Canvas Group in the scene.
		// If there are multiple canvas groups in the scene, this will return the wrong thing and not work.
		fadeGroup = FindObjectOfType<CanvasGroup>();

		//Fade is full opactity. 
		fadeGroup.alpha = 1;

		//Add button on-click events to the shop buttons.
		InitShop();

		// Add buttons on-click event to map zones.
		InitMap();


		//Set player's preferences (equipment & purchases)
		// this might need to be OnEquipment button.
		OnInventorySelect(SaveManager.Instance.state.activeEquipment);
		SetEquipment(SaveManager.Instance.state.activeEquipment);

		OnShopSelect (SaveManager.Instance.state.activeShopItem);
		SetPurchase (SaveManager.Instance.state.activeShopItem);
	
		//Make the buttons bigger for the selected item.
		inventoryPanel.GetChild(SaveManager.Instance.state.activeEquipment).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

		shopPanel.GetChild(SaveManager.Instance.state.activeShopItem).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        if (SaveManager.Instance.state.firstTime == true) {
            dialogBox.SetActive(true);
            SaveManager.Instance.state.firstTime = false;
            SaveManager.Instance.Save();
        } else {
            dialogBox.SetActive(false);
        }
	}

	private void Update() 
	{
		// Fade-in
		fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

		// Menu Navigation (smooth)
		// If you want the speed of the transition to be faster increase 0.1f, to slow it, decrease 0.1f
		menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);

        //Entering level zoomDuration.
        if (isEnteringLevel)
		{
			// Add to the zoomTransition float.
			zoomTransition += (1/zoomDuration) * Time.deltaTime;

			// Change the scale, followingthe AnimationCurve.
			menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, enteringLevelZoomCurve.Evaluate(zoomTransition));

            // Display char info
            displayCharacterInfo();

            //Change the desired position of the canvas, so it can follow the scale up.
            //This zooms in the center.
            Vector3 newDesiredPosition = desiredMenuPosition * 5;
			//This adds to the specific position of the level on the canvas.
			RectTransform rt = mapPanel.GetChild(Manager.Instance.currentLevel).GetComponent<RectTransform>();
			newDesiredPosition -= rt.anchoredPosition3D * 5;

			//this line will override the previous position Update.
			menuContainer.anchoredPosition3D = Vector3.Lerp(desiredMenuPosition,newDesiredPosition,enteringLevelZoomCurve.Evaluate(zoomTransition));

			// fade to white screen, this will override the first line of the update. 
			fadeGroup.alpha = zoomTransition;

			//Are we done with the animation
			if(zoomTransition >= 1)
			{
				//Enter the level.
				SceneManager.LoadScene("Level1");
			} 
		}
	}

   
    /*********************
    ***    USER  UI    ***
    **********************/
    private void displayCharacterInfo() {
        // Calculating the daily step progress with 500 steps a day
        stepsText.text = SaveManager.Instance.state.dailyStepCounts.ToString();
        progressSteps.text = calculateProgress(SaveManager.Instance.state.dailyStepCounts, SaveManager.Instance.state.maxDailySteps).ToString() + "%";

        // Calculating the step progress into levelling with 1000 steps/level
        stepsToLvl.text = (SaveManager.Instance.state.maxLevelSteps - SaveManager.Instance.state.dailyStepCounts).ToString();
        progressLvl.text = calculateProgress(SaveManager.Instance.state.dailyStepCounts, SaveManager.Instance.state.maxLevelSteps).ToString() + "%";

        // Displaying the character's level and name
        level.text = SaveManager.Instance.state.levelCrusade.ToString();
        heroName.text = SaveManager.Instance.state.username.ToString();
    }

	private int calculateProgress(int steps, int total) {
        return Mathf.CeilToInt(steps/total);
    }

   
    /*********************
    ***  INITIALIZERS  ***
    **********************/
    private void InitShop()
	{
		// Just make sure we've assigned the references.
		if (inventoryPanel == null || shopPanel == null)
			Debug.Log ("You did not assign the inventory/ shop panel in the inspector");

		// For every children transform under our inventory panel, find the button and add the on click event.
		int i = 0;
		foreach (Transform t in inventoryPanel) 
		{
			int currentIndex = i;

			Button b = t.GetComponent<Button>();
			b.onClick.AddListener (() => OnInventorySelect(currentIndex));

			// Set the color of the image, based on if owned or not.
			Image img = t.GetComponent<Image>();
			img.color = SaveManager.Instance.IsEquipmentOwned (i) ? Manager.Instance.playerColors[currentIndex] : Color.Lerp(Manager.Instance.playerColors[currentIndex], new Color(0,0,0,1),0.25f);

			i++;

		}

		//Resent index;
		i = 0;
		// Do the same for the shop panel.
		foreach (Transform t in shopPanel)
		{
			int currentIndex = i;

			Button b = t.GetComponent<Button> ();
			b.onClick.AddListener (() => OnShopSelect (currentIndex));

			// Set the color of the image, based on if owned or not.
			Image img = t.GetComponent<Image>();
			img.color = SaveManager.Instance.IsShopItemOwned (i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

			i++;

		}

	}

	private void InitMap()
	{
		// Just make sure we've assigned the references.
		if (mapPanel == null)
			Debug.Log ("You did not assign the map panel in the inspector");

		// For every children transform under our map panel, find the button and add the on click event.
		int i = 0;
		foreach (Transform t in mapPanel) 
		{
			int currentIndex = i;
		
			Button b = t.GetComponent<Button> ();
			b.onClick.AddListener (() => OnMapSelect (currentIndex));

			Image img = t.GetComponent<Image>();

			//is it unlocked?
			if(i <=SaveManager.Instance.state.completedLevel)
			{
				// It is unlocked!
				if(i == SaveManager.Instance.state.completedLevel)
				{
					// Its not completed!
					img.color = Color.white;
				}
				else
				{
					//level is already completed
					img.color = Color.green;
				}
			}
			else
			{
				// Level isn't unlocked. disable the button. can't click on it
				b.interactable = false;

				//Set to a dark color.
				img.color = Color.grey;
			}

			i++;

		}
	}

    
    /*********************
    ***  ITEM MANAGER  ***
    **********************/
    private void OnInventorySelect (int currentIndex)
	{
		Debug.Log ("Selecting Inventory Item : " + currentIndex);

		//if the button clcicked is already selected, exit
		if (selectedInventoryIndex == currentIndex)
			return;

		// Make the icon slightly bigger if it's selcted.
		inventoryPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
		//Put the previous one on normal scale
		inventoryPanel.GetChild(selectedInventoryIndex).GetComponent<RectTransform>().localScale = Vector3.one;


		// Set the selected equipment
		selectedInventoryIndex = currentIndex;

		//Change the content of the equip button, depending on the state of the equipent.
		if (SaveManager.Instance.IsEquipmentOwned (currentIndex))
		{
			//Equipement is owned.
			// Is it currently equipped? 
			if(activeInventoryIndex == currentIndex)
			{
				inventoryEquipButton.text = "Current";
			}
				inventoryEquipButton.text = "Select";
		} else 
		{
			//Equipment is not owned.
			inventoryEquipButton.text = "Buy: " + inventoryCost[currentIndex].ToString();
		}
	}

	private void OnShopSelect (int currentIndex)
	{
		Debug.Log ("Selecting Shop Item : " + currentIndex);

		//if the button clcicked is already selected, exit
		if (selectedPurchaseIndex == currentIndex)
			return;

		// Make the icon slightly bigger if it's selcted.
		shopPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
		//Put the previous one on normal scale
		shopPanel.GetChild(selectedPurchaseIndex).GetComponent<RectTransform>().localScale = Vector3.one;


		// Set the selected shop items
		selectedPurchaseIndex = currentIndex;

		//Change the content of the purchase button, depending on the state of the shop item.
		if (SaveManager.Instance.IsShopItemOwned (currentIndex))
		{
			//Shop item is owned.
			if (activePurchaseIndex == currentIndex)
			{
				shopPurchaseButton.text = "Current";
			}
				shopPurchaseButton.text = "Select";
		} else
			 
		{
			//shop item is not owned.
			shopPurchaseButton.text = "Buy: " + shopCost[currentIndex].ToString();
		}

		}

	private void OnMapSelect (int currentIndex)
	{
		Manager.Instance.currentLevel = currentIndex;
		//Debug.Log ("Selecting QualityLevel : " + currentIndex);
		isEnteringLevel = true;
	}

	public void OnEquipButton()
	{
		Debug.Log ("Equip this piece from inventory");	

		// Is the selected equipment owned.
		if (SaveManager.Instance.IsEquipmentOwned (selectedInventoryIndex)) {
			//Set the equipment
			SetEquipment (selectedInventoryIndex);
		} 
		else 
		{
			// If it is not owned. Attempt to buy the equipment. 
			if(SaveManager.Instance.BuyEquip(selectedInventoryIndex, inventoryCost[selectedInventoryIndex]))
				{
					// Success!
					SetEquipment(selectedInventoryIndex);

					//Whenever you have bought something. Change the color of the button.
					inventoryPanel.GetChild(selectedInventoryIndex).GetComponent<Image>().color = Manager.Instance.playerColors[selectedInventoryIndex];
					
					
					// Update the gold text.
					UpdateGoldText();

				}

				else

				{
					//Do not have enough gold!
					// Play sound feedback
					Debug.Log("Not enough Gold");
				}
		}
	}

	public void OnPurchaseButton()
	{
		Debug.Log ("Purchase this piece from the shop");


		// Is the selected shopItem owned.
		if (SaveManager.Instance.IsShopItemOwned (selectedPurchaseIndex)) {
			//Set the shopItem
			SetPurchase (selectedPurchaseIndex);
		} 
		else 
		{
			// If it is not owned. Attempt to buy the shop Item. 
			if(SaveManager.Instance.BuyShopItem(selectedPurchaseIndex, shopCost[selectedPurchaseIndex]))
			{
				// Success!
				SetPurchase(selectedPurchaseIndex);

				//Whenever you have bought something. Change the color of the button.
				shopPanel.GetChild (selectedPurchaseIndex).GetComponent<Image>().color = Color.white;

				// Update the gold text.
				UpdateGoldText();
				}
			else

			{
				//Do not have enough gold!
				// Play sound feedback
				Debug.Log("Not enough Gold");
			}
		}
	}

    
    /*********************
    ***   NAVIGATION   ***
    **********************/
	private void SetCameraTo(int menuIndex)
	{

		NavigateTo(menuIndex);
		menuContainer.anchoredPosition3D = desiredMenuPosition;
		
	}

	private void NavigateTo (int menuIndex)
	{	
		// 0 && default xase = main menu.
		switch (menuIndex) 
		{
		default:
		case 0:
			desiredMenuPosition = Vector3.zero;
			menuCam.BackToMainMenu();
			break;
		// 1 = Equipment
		case 1:
			// In future iterations, 1600 might need to be changed to screen width when the ratio changes. 
			desiredMenuPosition = Vector3.right * 1280;
			menuCam.MoveToShop();
			break;
		//2 = Map
		case 2:
			desiredMenuPosition = Vector3.left * 1280;
            menuCam.MoveToLevel();
			break;

		case 3:
			desiredMenuPosition = Vector3.down * 800;
			menuCam.MoveToDial();
			break;

		case 4: 
			desiredMenuPosition = Vector3.up * 800;
			menuCam.MoveToSettings();
			break;
			// Future cases for up will require a Vector 3 to be multiplied by 800 (the screen height)
		}
	}

	private void SetEquipment(int index)
	{
		//Set the active index of inventory
		activeInventoryIndex = index;
		SaveManager.Instance.state.activeEquipment = index;

		// Chnage the selected equipment on the player model
		//Manager.Instance.playerMaterial.color = Manager.Instance.playerColors[index];

		//Change the selected skin on the player model (SL)
		Manager.Instance.playerMaterial.mainTexture = Manager.Instance.playerTextures[index];


		// Change equip button text
		inventoryEquipButton.text = "Current";

		//Remember Preferences.
		SaveManager.Instance.Save();
	}

	private void SetPurchase(int index)
	{
		//Set the active index of shopItems
		activePurchaseIndex = index;
		SaveManager.Instance.state.activeShopItem = index;

		// Change the selected shop item on the player model.

		// Change purchase button text. 
		shopPurchaseButton.text = "Current";

		//Remember preferences
		SaveManager.Instance.Save();
	}

	private void UpdateGoldText()
	{
		goldText.text = SaveManager.Instance.state.gold.ToString ();
	}

    public void acceptGift() {
        SaveManager.Instance.state.firstTime = false;
        // Play "ding" noise
        dialogBox.SetActive(false);
    }

    /*********************
    ***     BUTTONS    ***
    **********************/
    public void OnEquipmentClick()
	{
		NavigateTo (1);
	}

	public void OnMapClick() 
	{
		NavigateTo (2);
	}

	public void OnBackClick()
	{
		NavigateTo (0);
	}

	public void OnDialClick()
	{
		//Debug.Log ("I've clicked");
		NavigateTo (3);

	}
	public void OnSettingsClick()
	{
		NavigateTo (4);
	}

    public void loadLevel() {
        SceneManager.LoadScene("Level1");
    }

    public void loadCreateAccount() {
        SceneManager.LoadScene("UserAuthentication");
    }

    public void loadMP() {
        SceneManager.LoadScene("powerUpsTestScene");
    }
}
