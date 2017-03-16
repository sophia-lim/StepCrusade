using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour 
{
	// So you can access the save state anywhere in the game or when you want to quickly save on the go.
	public static SaveManager Instance { set; get;}
	public SaveState state;

	private void Awake () 
	{
		
		DontDestroyOnLoad (gameObject);
		Instance = this;
		Load ();
		/*
		//Testing. This can be deleted.
		Debug.Log (Helper.Serialize<SaveState>(state));

		Debug.Log (state.equipmentOwned);
		UnlockEquipment (0);
		Debug.Log (state.equipmentOwned);
		UnlockEquipment (1);*/

	}


	// Save the whole state of this saveState script to the player pref
	public void Save()
	{
		//Serialize Save State
		PlayerPrefs.SetString("save",Helper.Serialize<SaveState>(state));
	}

	// Load the previous save state from the player prefs
	public void Load() 
	{
		if(PlayerPrefs.HasKey("save"))
		{
			// Deserialize Save State
			state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));

		} else {
			state = new SaveState ();
			Save ();
			Debug.Log ("No Save File found creating a new one");
		}
	}

	// Check if the equipment is owned
	public bool IsEquipmentOwned (int index)
	{
		// Checking bits.
		//If you have 16 items it would look like this: 0000 0000 0000 0000.
		// If you unlock the first piece of equipment in the list it'll look like this: 0000 0000 0000 0001.
		// After unlocking the first piece equipment you want to unlock the fourth piece it'll look like this: 0000 0000 0000 1001.

		// Check if the bit is set, if so the equipment is owned
		return (state.equipmentOwned & (1 << index)) != 0;

	}

	public bool IsShopItemOwned (int index)
	{

		// Check if the bit is set, if so the shop item is owned
		return (state.shopItemOwned & (1 << index)) != 0;
	}

	//Attempt buying equipment, return true/ false 
	public bool BuyEquip (int index, int cost) 
	{
		if (state.gold >= cost) 
		{
			//enough money, remove the amount from the current gold stack.
			state.gold -= cost;
			UnlockEquipment (index);

			// Save progress & changes made.
			Save ();

			return true;
		} 
		else 
		{
			// Not enough monwy, return false
			return false;
		}
	}


	//Attempt buying shopitem, return true/ false 
	public bool BuyShopItem (int index, int cost) 
	{
		if (state.gold >= cost) 
		{
			//enough money, remove the amount from the current gold stack.
			state.gold -= cost;
			UnlockShopItem (index);

			// Save progress & changes made.
			Save ();

			return true;
		} 
		else 
		{
			// Not enough monwy, return false
			return false;
		}
	}

	// Unloack a piece of equipment in the "equipmentOwned" int
	public void UnlockEquipment(int index) 
	{
		// Toggle on the bit at index
		// Toggle on opperator : |=
		//Toggle off opperator : ^=
		state.equipmentOwned |= 1 << index;
	}

	public void UnlockShopItem(int index) 
	{
		// Toggle on the bit at index
		state.shopItemOwned |= 1 << index;
	}

	// Complete level
	public void CompleteLevel(int index)
	{
		// if this is the current active level
		if(state.completedLevel == index)
		{
			state.completedLevel++;
			Save();
		}
	}

	// Reset the whole save file
	public void ResetSave()
	{
		PlayerPrefs.DeleteKey ("save");
	}
}
