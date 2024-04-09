using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed += Interact; }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed -= Interact; }

    public virtual void Interact() {}
}
