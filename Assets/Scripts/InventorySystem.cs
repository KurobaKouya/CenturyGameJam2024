using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onItemPickedUp += PickupItem;
        GameEvents.onItemDropped += DropItem;
        GameEvents.onRelicDropped += DropRelic;
    }


    private void OnDisable()
    {
        GameEvents.onItemPickedUp -= PickupItem;
        GameEvents.onItemDropped -= DropItem;
        GameEvents.onRelicDropped -= DropRelic;
    }


    private void PickupItem(Globals.ItemIndex itemId, Interactable item)
    {
        // Check if item can be picked up
        if (GameManager.Instance.gameData.itemInHand != Globals.ItemIndex.None)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        Debug.Log("Picked up " + itemId.ToString());

        // Add item to empty slot
        if (GameManager.Instance.gameData.itemInHand == Globals.ItemIndex.None)
        {
            GameManager.Instance.gameData.itemInHand = itemId;
            item.DisableInteract();
            Destroy(item.gameObject);
        }
    }


    private void DropItem(Globals.ItemIndex itemId)
    {
        
    }


    private void DropRelic()
    {

    }
}
