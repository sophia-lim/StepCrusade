﻿using System.Collections;
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

	public Text inventoryEquipButton;
	public Text shopPurchaseButton;

	//Setting the price for the actual items in the inventory
	private int[] inventoryCost = new int[] {0, 5, 5, 5, 10, 10, 10, 15, 15, 10 };
	// Setting the proce for the actual items in the shop
	private int[] shopCost = new int[] {0, 20, 40, 40, 60, 60, 80, 85, 85, 100 };
	private int selectedInventoryIndex;
	private int selectedPurchaseIndex; 

	public Text goldText;
	private int activeInventoryIndex;
	private int activePurchaseIndex;


	private Vector3 desiredMenuPosition;

	private void Start () 
	{
		//$$ TEMPORARY....Temporarily setting the amount of gold the player starts with to 999.
		SaveManager.Instance.state.gold = 999;

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
		// this mightneed to be OnEquipment button.
		OnInventorySelect(SaveManager.Instance.state.activeEquipment);
		SetEquipment(SaveManager.Instance.state.activeEquipment);

		OnShopSelect (SaveManager.Instance.state.activeShopItem);
		SetPurchase (SaveManager.Instance.state.activeShopItem);
	
		//Make the buttons bigger for the selected item.
		inventoryPanel.GetChild(SaveManager.Instance.state.activeEquipment).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

		shopPanel.GetChild(SaveManager.Instance.state.activeShopItem).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
	}

	private void Update() 
	{
		// Fade-in
		fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

		// Menu Navigation (smooth)
		// If you want the speed of the transition to be faster increase 0.1f, to slow it, decrease 0.1f
		menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
	}

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
			img.color = SaveManager.Instance.IsEquipmentOwned (i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

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

			i++;

		}


		
	}

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
				inventoryEquipButton.text ="Current";
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
		Debug.Log ("Selecting QualityLevel : " + currentIndex);
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
					inventoryPanel.GetChild(selectedInventoryIndex).GetComponent<Image>().color = Color.white;
					
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

	private void NavigateTo (int menuIndex)
	{	
		// 0 && default xase = main menu.
		switch (menuIndex) 
		{
		default:
		case 0:
			desiredMenuPosition = Vector3.zero;
			break;
		// 1 = Map menu
		case 1:
			// In future iterations, 1600 might need to be changed to screen width when the ratio changes. 
			desiredMenuPosition = Vector3.right * 1280;
			break;
		//2 = Customization menu
		case 2:
			desiredMenuPosition = Vector3.left * 1280;
			break;

			// Future cases for up will require a Vector 3 to be multiplied by 900 (the screen height)
		}
	}

	private void SetEquipment(int index)
	{
		//Set the active index of inventory
		activeInventoryIndex = index;
		SaveManager.Instance.state.activeEquipment = index;

		// Chnage the selected equipment on the player model

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


	//Buttons
	public void OnEquipmentClick()
	{
		NavigateTo (2);
	}

	public void OnMapClick() 
	{
		NavigateTo (1);
	}

	public void OnBackClick()
	{
		NavigateTo (0);
	}

    //Sophia: Go to sign up scene
    public void Register() {
        SceneManager.LoadScene("Register");
    }    
    
    //Sophia: Go to forgot password scene
    public void Password() {
        SceneManager.LoadScene("Password");
    }

}
