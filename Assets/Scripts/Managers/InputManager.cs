using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Player player;
    private PlayerInput playerInput;
    private bool playerInputEnabled = true;


    public void UpdateLoop()
    {
        if (player) if (playerInputEnabled) UpdatePlayerInput(); else ResetPlayerInput();
    }


    private void UpdatePlayerInput()
    {
        player.movementDir = playerInput.Movement.Keyboard.ReadValue<Vector2>();
    }


    private void ResetPlayerInput()
    {
        player.movementDir = Vector2.zero;
    }


    public void Init()
    {
        playerInput = new();
        playerInput.Enable();
    }


    private void OnDestroy()
    {
        playerInput.Disable();
    }

    #region GlobalEvents
    private void OnEnable()
    {
        GlobalEvents.onPlayerEnabled += (sender) => player = sender;
        GlobalEvents.onEnablePlayerMovement += (bool enable) => { playerInputEnabled = enable; };
    }


    private void OnDisable()
    {
        GlobalEvents.onPlayerEnabled -= (sender) => player = sender;
        GlobalEvents.onEnablePlayerMovement -= (bool enable) => { playerInputEnabled = enable; };
    }
    #endregion
}
