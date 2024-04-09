using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed += Interact; UIEvents.Instance.OpenInteractUI(true); }
    private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed -= Interact; UIEvents.Instance.OpenInteractUI(false); }

    public virtual void Interact() {}
}
