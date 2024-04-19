using FischlWorks_FogWar;
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

    [SerializeField] private AttackHitBox attackHitBox = null;
    [SerializeField] private CameraShakeEvent vfx;
    [SerializeField] GameObject flashlight;
    public float lightRadius;

    [Header("SFX")]
    [SerializeField] AudioClipInstance hurtSFX, swingAud, noStaminaAud;

    

    public Animator animator;


    // Player Setup + Event subscribing
    private void OnEnable()
    {
        GlobalEvents.Instance.PlayerEnabled(this);
        InputEvents.onToggleSprint += (bool enable) => isSprinting = enable;
        InputEvents.onPlayerAttack += Attack;
        //set player light
        GameManager.Instance.gameData.playerFog = new NoFogPosition(transform.position, lightRadius, 0, Globals.playerHealth / Globals.healthDrainSpeed, null, true, true);
        GameEvents.onPlayerHit += () => AudioManager.instance.PlaySourceAudio(hurtSFX);
        FindObjectOfType<MinimapToFog>().AddNoFog(GameManager.Instance.gameData.playerFog);
        if (!rb) rb = GetComponent<Rigidbody>();
    }


    private void OnDisable()
    {
        InputEvents.onToggleSprint -= (bool enable) => isSprinting = enable;
        InputEvents.onPlayerAttack -= Attack;
        GameEvents.onPlayerHit -= () => AudioManager.instance.PlaySourceAudio(hurtSFX); 
        FindObjectOfType<MinimapToFog>().RemoveNoFog(GameManager.Instance?.gameData.playerFog);
    }


    public void UpdateLoop()
    {
        // Movement
        rb.velocity = new Vector3(movementDir.x * movementSpeed, 0, movementDir.y * movementSpeed);
        // Rotation
        LookAtCursor();

        // Stamina
        UpdateStamina();

        // Light
        UpdateLight();


        //while in light
        WhileInLight();
    }


    void UpdateWeapon()
    {
        animator.SetLayerWeight(1, canAttack ? 1 : 0);
    }

    //player light
    void UpdateLight()
    {
        Debug.Log("PlayerLight1" + GameManager.Instance.gameData.playerFog.time + " / " + Globals.playerHealth / Globals.healthDrainSpeed);
        Debug.Log("PlayerLight2: " + GameManager.Instance.gameData.playerFog.currentSize);
        Debug.Log("PlayerLight3: " + GameManager.Instance.gameData.playerFog.position + " " + transform.position);
        GameManager.Instance.gameData.playerFog.position = transform.position;
        
        flashlight.gameObject.SetActive(GameManager.Instance.flashlightToggled);
        if (GameManager.Instance.inUnknown)
        {
            if (!GameManager.Instance.flashlightToggled)
            {
                GameManager.Instance.gameData.playerFog.noDecay = false;
            }
        }
        else
        {
            GameManager.Instance.gameData.playerFog.noDecay = true;
            GameManager.Instance.gameData.playerFog.ResetTime();
        }
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
        //if (attackHitBox) attackHitBox.gameObject.SetActive(true);
        animator.SetTrigger("Attack");
        attackHitBox.damage = Globals.playerDmg;

        yield return new WaitForSeconds(1);
        
        //if (attackHitBox) attackHitBox.gameObject.SetActive(false);
        canAttack = true;
        canRegenStamina = true;
    }

    //Axe swing
    public void SwingStart(bool state)
    {
        Debug.Log("AAA " + state);
        attackHitBox.gameObject.SetActive(state);
        
        if (state)
        {
            AudioManager.instance.PlaySourceAudio(swingAud, transform.position);
            vfx.Shake();
        }
    

    }




    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            GameManager.Instance.inUnknown = false;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            GameManager.Instance.inUnknown = true;
        }
    }

    void WhileInLight()
    {
        if (!GameManager.Instance.inUnknown)
        {
            GameManager.Instance.gameData.playerHealth = Globals.playerHealth;
            GameManager.Instance.gameData.playerFog.ResetTime();
        }
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

    public void ResetPlayerStats()
    {
        GameManager.Instance.gameData.playerFog.ResetTime();
    }
}
