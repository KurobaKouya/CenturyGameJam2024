using UnityEngine;

public class InteractLabel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        UIEvents.onInteractableUI += EnableUI;
    }


    private void OnDisable()
    {
        UIEvents.onInteractableUI -= EnableUI;
    }


    private void Start()
    {
        canvasGroup.alpha = 0;
    }


    private void EnableUI(bool enable)
    {
        if (enable) canvasGroup.alpha = 1;
        else canvasGroup.alpha = 0;
    }
}
