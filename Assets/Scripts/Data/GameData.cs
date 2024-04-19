using UnityEngine;

[System.Serializable]
public class GameData
{
    public int relicsCollected = 0;
    public int lives = 3;
    public float playerHealth = 100f;
    public float flashlightPower = 100f;
    public Globals.ItemIndex itemInHand = Globals.ItemIndex.None;
    public bool relicInInventory = false;
    public float inkAmount = 0f;
    //player light
    public NoFogPosition playerFog = new NoFogPosition();

    // public GameData()
    // {
    //     relicsCollected = 0;
    //     lives = 5;
    //     flashlightPower = 100f;
    //     itemInHand = Globals.ItemIndex.None;
    //     relicInInventory = false;
    // }
}
