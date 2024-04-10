using System;
using UnityEngine;

public class GameEvents : Singleton<GameEvents>
{
    public static event Action<Globals.ItemIndex, Interactable> onItemPickedUp = delegate {};
    public void PickUpItem(Globals.ItemIndex itemId, Interactable item) => onItemPickedUp?.Invoke(itemId, item);
}
