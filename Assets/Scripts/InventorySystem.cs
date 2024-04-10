using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Globals.ItemIndex firstItem = Globals.ItemIndex.None;
    [SerializeField] private Globals.ItemIndex secondItem = Globals.ItemIndex.None;


    private void OnEnable()
    {
        GameEvents.onItemPickedUp += PickupItem;
    }


    private void OnDisable()
    {
        GameEvents.onItemPickedUp -= PickupItem;
    }


    private void PickupItem(Globals.ItemIndex itemId, Interactable item)
    {
        // Check if item can be picked up
        if (firstItem != Globals.ItemIndex.None && secondItem != Globals.ItemIndex.None)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        Debug.Log("Picked up " + itemId.ToString());

        // Add item to empty slot
        if (firstItem == Globals.ItemIndex.None)
        {
            firstItem = itemId;
            item.DisableInteract();
            Destroy(item.gameObject);
        }
        else if (secondItem == Globals.ItemIndex.None)
        {
            secondItem = itemId;
            item.DisableInteract();
            Destroy(item.gameObject);
        }
    }
}
