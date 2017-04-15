public class SaveState 
{

    // Player information
    public string username = "";
    public string email = "";
    public bool firstTime = true;
    public bool hasRegistered = false;

    // Index for skin/rags
    public int currentSkin = 0;

    // The amount of gold the player has.
    public int gold = 100;

	// The completed levels. If you complete level 2 you can access level 3.
	public int equipmentOwned = 0;

	//
	public int shopItemOwned = 0;
	public int completedLevel = 0;

	public int activeEquipment = 0;
	public int activeShopItem = 0;

    // Powerup 1: Countdown at zero; start with zero motion potion
    public int motionPotionCountdown = 0;
    public int motionPotionCount = 0;

    // Powerup 2: Countdown at zero; start with zero strong heart
    public int strongHeartCountdown = 0;
    public int strongHeartCount = 0;
	

}
