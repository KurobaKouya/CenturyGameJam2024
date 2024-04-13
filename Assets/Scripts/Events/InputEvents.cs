using System;

public class InputEvents : Singleton<InputEvents>
{
    public static event Action onInteractPressed = delegate {};
    public void Interact() => onInteractPressed?.Invoke();


    // public static event Action onToggleFlashlight = delegate {};
    // public void ToggleFlashlight() => onToggleFlashlight?.Invoke();
}
