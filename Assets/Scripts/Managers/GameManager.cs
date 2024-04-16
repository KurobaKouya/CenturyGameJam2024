using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public GameData gameData = null;
    [HideInInspector] public Player player { get; private set; }
    [HideInInspector] public bool flashlightToggled = false;
    [HideInInspector] public bool inUnknown = false;


    public void Init()
    {
        gameData ??= new();
    }


    public void UpdateLoop()
    {
        if (player) player.UpdateLoop();
        /*if (flashlightToggled) */UpdatePlayerStats();
    }


    private void UpdatePlayerStats()
    {
        // Flashlight
        // ...
        // Drain power when in unknown else regen
        gameData.flashlightPower = inUnknown ? gameData.flashlightPower - Globals.flashlightDrain * Time.deltaTime : gameData.flashlightPower + Globals.flashlightRegen * Time.deltaTime;
        gameData.flashlightPower = Mathf.Clamp(gameData.flashlightPower, 0, 100);

        flashlightToggled = gameData.flashlightPower > 0 ? true : false;
        // if (gameData.flashlightPower <= 0f)
        // {
        //     flashlightToggled = false;
        //     return;
        // }

        // Powerdrain
        // gameData.flashlightPower -= Globals.flashlightDrain * Time.deltaTime;

        // Health
        // ...
        gameData.playerHealth += Globals.healthRegen * Time.deltaTime;
        gameData.playerHealth = Mathf.Clamp(gameData.playerHealth, 0, 100);
    }


    private void GameWin()
    {
        Debug.Log("YOU WIN");
    }


    private void PlayerHit()
    {
        if (gameData.flashlightPower >= Globals.flashlightDrainOnHit)
            gameData.flashlightPower -= Globals.flashlightDrainOnHit;
        else
            gameData.playerHealth -= Globals.monsterDmg;

        gameData.flashlightPower = Mathf.Clamp(gameData.flashlightPower, 0, 100);

        if (gameData.playerHealth <= 0) StartCoroutine(PlayerDeath());

        Debug.Log("Flashlight: " + gameData.flashlightPower);
        Debug.Log("Health: " + gameData.playerHealth);
    }


    IEnumerator PlayerDeath()
    {
        gameData.lives -= 1;
        gameData.flashlightPower = 100f;
        gameData.playerHealth = 100f;
        // Drop relic in inventory
        GameEvents.Instance.DropRelic();

        yield return null;

        // Set player position to spawn        
    }


    #region Events
    private void OnEnable()
    {
        GlobalEvents.onPlayerEnabled += (sender) => player = sender;
        GameEvents.onPlayerHit += PlayerHit;
        // InputEvents.onToggleFlashlight += () => flashlightToggled = !flashlightToggled;
        GameEvents.onEnterUnknown += () => inUnknown = true;
        GameEvents.onExitUnknown += () => inUnknown = false;
        GameEvents.onAllRelicsCollected += GameWin;
    }


    private void OnDisable()
    {
        GlobalEvents.onPlayerEnabled -= (sender) => player = sender;
        GameEvents.onPlayerHit -= PlayerHit;
        // InputEvents.onToggleFlashlight -= () => flashlightToggled = !flashlightToggled;
        GameEvents.onEnterUnknown -= () => inUnknown = true;
        GameEvents.onExitUnknown -= () => inUnknown = false;
        GameEvents.onAllRelicsCollected -= GameWin;
    }
    #endregion
}
