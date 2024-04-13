using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private Globals.ItemIndex itemInHand = Globals.ItemIndex.None;


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
        if (itemInHand != Globals.ItemIndex.None)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        Debug.Log("Picked up " + itemId.ToString());

        // Add item to empty slot
        if (itemInHand == Globals.ItemIndex.None)
        {
            itemInHand = itemId;
            item.DisableInteract();
            Destroy(item.gameObject);
        }
    }
}
