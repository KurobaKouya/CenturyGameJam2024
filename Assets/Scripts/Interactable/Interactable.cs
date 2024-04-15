using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool playerInTrigger = false;


    private void OnTriggerEnter(Collider other) 
    { 
        if (playerInTrigger) return;
        if (other.CompareTag("Player")) 
        {
            InputEvents.onInteractPressed += Interact; 
            UIEvents.Instance.OpenInteractUI(true);
            playerInTrigger = true;
        }
    }


    private void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag("Player")) 
        {
            InputEvents.onInteractPressed -= Interact; 
            UIEvents.Instance.OpenInteractUI(false);
            playerInTrigger = false;
        }
    }


    private void OnEnable()
    {
        if (playerInTrigger)
        {
            InputEvents.onInteractPressed += Interact; 
            UIEvents.Instance.OpenInteractUI(true);
        }
    }


    private void OnDisable()
    {
        InputEvents.onInteractPressed -= Interact; 
        UIEvents.Instance.OpenInteractUI(false);
    }


    public void DisableInteract() { InputEvents.onInteractPressed -= Interact; UIEvents.Instance.OpenInteractUI(false); }
    public virtual void Interact() {}
}
