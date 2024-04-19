using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public Globals.SceneIndex currentScene = Globals.SceneIndex.Preload;
    [HideInInspector] public GameData gameData = null;
    [HideInInspector] public Player player { get; private set; }
    [HideInInspector] public bool flashlightToggled = false;
     public bool inUnknown = false;
    [HideInInspector] public bool inMap = false;

    public void Init()
    {
        gameData ??= new();
    }


    public void UpdateLoop()
    {
        if (currentScene != Globals.SceneIndex.Game) return;
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
        if (!flashlightToggled)
            gameData.playerHealth -= Globals.healthDrainSpeed * Time.deltaTime;
        gameData.playerHealth += Globals.healthRegen * Time.deltaTime;
        gameData.playerHealth = Mathf.Clamp(gameData.playerHealth, 0, 100);

        //Ink
        //
        gameData.inkAmount = Mathf.Clamp(gameData.inkAmount, 0, Globals.maxInk);
        Debug.Log("Ink: " + gameData.inkAmount);
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
        {
            gameData.playerHealth -= Globals.monsterDmg;
            //player fog is lerped, so need to scale accordingly to how much time left
            gameData.playerFog.time += Globals.monsterDmg / Globals.playerHealth;
        }

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
        GameEvents.Instance.PlayerDeath();

        // Set player position to spawn
        player.transform.position = new(0, transform.position.y, 0);
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
