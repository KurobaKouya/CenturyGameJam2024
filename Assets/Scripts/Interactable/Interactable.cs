using UnityEngine;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed += Interact; UIEvents.Instance.OpenInteractUI(true); }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) InputEvents.onInteractPressed -= Interact; UIEvents.Instance.OpenInteractUI(false); }
    public void DisableInteract() { InputEvents.onInteractPressed -= Interact; UIEvents.Instance.OpenInteractUI(false); }
    public virtual void Interact() {}
}
