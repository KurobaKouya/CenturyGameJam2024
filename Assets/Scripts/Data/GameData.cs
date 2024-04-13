[System.Serializable]
public class GameData
{
    public int relicsCollected = 0;
    public int lives = 5;
    public float flashlightPower = 100f;
    public Globals.ItemIndex itemInHand = Globals.ItemIndex.None;
    public bool relicInInventory = false;


    // public GameData()
    // {
    //     relicsCollected = 0;
    //     lives = 5;
    //     flashlightPower = 100f;
    //     itemInHand = Globals.ItemIndex.None;
    //     relicInInventory = false;
    // }
}
