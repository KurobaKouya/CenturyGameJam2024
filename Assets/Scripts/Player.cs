using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Vector2 movementDir = Vector2.zero;
    public float movementSpeed = 10f;
    public bool isSprinting = false;
    public float stamina = 100f;
    private bool canRegenStamina = true;
    private bool canAttack = true;


    // Player Setup + Event subscribing
    private void OnEnable()
    {
        GlobalEvents.Instance.PlayerEnabled(this);
        InputEvents.onToggleSprint += (bool enable) => isSprinting = enable;
        InputEvents.onPlayerAttack += Attack;

        if (!rb) rb = GetComponent<Rigidbody>();
    }


    private void OnDisable()
    {
        InputEvents.onToggleSprint -= (bool enable) => isSprinting = enable;
        InputEvents.onPlayerAttack -= Attack;
    }


    public void UpdateLoop()
    {
        // Movement
        rb.velocity = new Vector3(movementDir.x * movementSpeed, 0, movementDir.y * movementSpeed);

        // Rotation
        LookAtCursor();

        // Stamina
        UpdateStamina();
    }


    private void UpdateStamina()
    {
        // Drain
        if (isSprinting) 
        {
            stamina -= Globals.sprintStaminaDrain * Time.deltaTime;
            movementSpeed = Globals.sprintMovementSpeed;
        }
        else movementSpeed = Globals.playerMovementSpeed;


        // Regen
        if (canRegenStamina && !isSprinting && stamina < 100) stamina += Globals.staminaRegen * Time.deltaTime;
    }


    private void Attack() 
    {
        if (!canAttack) return;
        if (stamina >= Globals.attackStaminaDrain && GameManager.Instance.gameData.itemInHand == Globals.ItemIndex.Axe) StartCoroutine(AttackCoroutine());
    }


    IEnumerator AttackCoroutine()
    {
        canAttack = false;
        canRegenStamina = false;
        stamina -= Globals.attackStaminaDrain;
        // Do attack
        // ...


        yield return new WaitForSeconds(1);
        canAttack = true;
        canRegenStamina = true;
    }


    private void LookAtCursor()
    {
        Ray cameraRay = CameraHandler.Instance.cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new(pointToLook.x, transform.position.y, pointToLook.z)), 10f * Time.deltaTime);

            // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(pointToLook), 0.5f * Time.deltaTime);
        }
    }
}
