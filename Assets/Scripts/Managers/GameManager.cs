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
        /*if (flashlightToggled) */UpdateFlashlight();
    }


    private void UpdateFlashlight()
    {
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
    }


    #region Events
    private void OnEnable()
    {
        GlobalEvents.onPlayerEnabled += (sender) => player = sender;
        GameEvents.onPlayerDeath += () => gameData.lives -= 1;
        GameEvents.onPlayerHit += () => gameData.flashlightPower -= Globals.flashlightDrainOnHit;
        // InputEvents.onToggleFlashlight += () => flashlightToggled = !flashlightToggled;
        GameEvents.onEnterUnknown += () => inUnknown = true;
        GameEvents.onExitUnknown += () => inUnknown = false;
    }


    private void OnDisable()
    {
        GlobalEvents.onPlayerEnabled -= (sender) => player = sender;
        GameEvents.onPlayerDeath -= () => gameData.lives -= 1;
        GameEvents.onPlayerHit -= () => gameData.flashlightPower -= Globals.flashlightDrainOnHit;
        // InputEvents.onToggleFlashlight -= () => flashlightToggled = !flashlightToggled;
        GameEvents.onEnterUnknown -= () => inUnknown = true;
        GameEvents.onExitUnknown -= () => inUnknown = false;
    }
    #endregion
}
