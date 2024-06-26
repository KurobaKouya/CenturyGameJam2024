using System;

public class UIEvents : Singleton<UIEvents>
{
    public static event Action<bool> onInteractableUI = delegate {};
    public void OpenInteractUI(bool enable) => onInteractableUI?.Invoke(enable);

    public static event Action onToggleInventory = delegate {};
    public void ToggleInventory() => onToggleInventory?.Invoke();

    public static event Action onToggleMap = delegate { };
    public void ToggleMap() => onToggleMap?.Invoke();


    public static event Action onTogglePause = delegate { };
    public void TogglePause() => onTogglePause?.Invoke();
}
