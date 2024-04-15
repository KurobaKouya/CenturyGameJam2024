using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField] private Globals.ItemIndex item = Globals.ItemIndex.None;


    public override void Interact()
    {
        if (item == Globals.ItemIndex.None)
        {
            Debug.LogError("Pickup Interaction Error: Item not defined.");
            return;
        }

        // Send event
        GameEvents.Instance.PickUpItem(item, this);
    }
}
