using System;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    public static event Action<Globals.ItemIndex, Interactable> onItemPickedUp = delegate{};
    public void PickUpItem(Globals.ItemIndex itemId, Interactable item) => onItemPickedUp?.Invoke(itemId, item);


    public static event Action onRelicDropped = delegate{};
    public void DropRelic() => onRelicDropped?.Invoke();


    public static event Action onPlayerDeath = delegate{};
    public void PlayerDeath() => onPlayerDeath?.Invoke();


    public static event Action onPlayerHit = delegate{};
    public void PlayerHit() => onPlayerHit?.Invoke();


    public static event Action onEnterUnknown = delegate{};
    public void EnterUnknown() => onEnterUnknown?.Invoke();


    public static event Action onExitUnknown = delegate{};
    public void ExitUnknown() => onExitUnknown?.Invoke();


    public static event Action onAllRelicsCollected = delegate{};
    public void AllRelicsCollected() => onAllRelicsCollected?.Invoke();
}
