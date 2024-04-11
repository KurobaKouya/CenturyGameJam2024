using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : Singleton<InputManager>
{
    private Player player;
    private PlayerInput playerInput;
    private bool playerInputEnabled = true;


    public void UpdateLoop()
    {
        if (player) if (playerInputEnabled) UpdatePlayerInput(); else ResetPlayerInput();
        if (Input.GetKeyDown(KeyCode.I)) UIEvents.Instance.ToggleInventory();
    }


    private void UpdatePlayerInput()
    {
        player.movementDir = playerInput.Movement.Keyboard.ReadValue<Vector2>();

        if (playerInput.UI.Interact.WasPressedThisFrame()) InputEvents.Instance.Interact();
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
