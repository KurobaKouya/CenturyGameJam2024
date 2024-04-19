using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    private Player player;
    private PlayerInput playerInput;
    private bool playerInputEnabled = true;


    public void UpdateLoop()
    {
        if (player) if (playerInputEnabled) UpdatePlayerInput(); else ResetPlayerInput();
        // if (Input.GetKeyDown(KeyCode.I)) UIEvents.Instance.ToggleInventory();
        if (playerInput.Controls.TogglePause.WasPressedThisFrame()) UIEvents.Instance.TogglePause();
    }


    private void UpdatePlayerInput()
    {
        Vector2 movementDir = playerInput.Controls.Movement.ReadValue<Vector2>();  
        player.movementDir = movementDir;

        // Enabling sprint
        // ...
        if (movementDir != Vector2.zero) if (playerInput.Controls.Sprint.WasPressedThisFrame()) InputEvents.Instance.ToggleSprint(true);
        // Disabling sprint
        // ...
        if (playerInput.Controls.Sprint.WasReleasedThisFrame()) InputEvents.Instance.ToggleSprint(false);


        // Attacking
        if (playerInput.Controls.Attack.WasPressedThisFrame()) InputEvents.Instance.PlayerAttack();

        if (playerInput.Controls.ToggleMap.WasPressedThisFrame()) UIEvents.Instance.ToggleMap();


        // Picking up & dropping items
        if (playerInput.Interactions.Interact.WasPressedThisFrame()) InputEvents.Instance.Interact();
        if (playerInput.Interactions.Drop.WasPressedThisFrame())     InputEvents.Instance.DropItem();

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
