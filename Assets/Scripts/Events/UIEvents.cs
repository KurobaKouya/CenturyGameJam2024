using System;

public class UIEvents : Singleton<UIEvents>
{
    public static event Action<bool> onInteractableUI = delegate {};
    public void OpenInteractUI(bool enable) => onInteractableUI?.Invoke(enable);

    public static event Action onToggleInventory = delegate {};
    public void ToggleInventory() => onToggleInventory?.Invoke();
}
