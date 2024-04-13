using System;

public class InputEvents : Singleton<InputEvents>
{
    public static event Action<bool> onToggleSprint = delegate{};
    public void ToggleSprint(bool enable) => onToggleSprint?.Invoke(enable);


    public static event Action onInteractPressed = delegate{};
    public void Interact() => onInteractPressed?.Invoke();


    public static event Action onDropItemPressed = delegate{};
    public void DropItem() => onDropItemPressed?.Invoke();


    public static event Action onPlayerAttack = delegate{};
    public void PlayerAttack() => onPlayerAttack?.Invoke();

    // public static event Action onToggleFlashlight = delegate {};
    // public void ToggleFlashlight() => onToggleFlashlight?.Invoke();
}
