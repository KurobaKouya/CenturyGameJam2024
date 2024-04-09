using System;

public class InputEvents : Singleton<InputEvents>
{
    public static event Action onInteractPressed = delegate {};
    public void Interact() => onInteractPressed?.Invoke();
}
