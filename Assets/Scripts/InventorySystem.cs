using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onItemPickedUp += PickupItem;
        InputEvents.onDropItemPressed += DropItem;
        GameEvents.onRelicDropped += DropRelic;
    }


    private void OnDisable()
    {
        GameEvents.onItemPickedUp -= PickupItem;
        InputEvents.onDropItemPressed -= DropItem;
        GameEvents.onRelicDropped -= DropRelic;
    }


    private void PickupItem(Globals.ItemIndex itemId, Interactable item)
    {
        if (itemId == Globals.ItemIndex.Relic)
        {
            if (GameManager.Instance.gameData.relicInInventory)
            {
                Debug.Log("Already carrying a relic!");
                return;
            }

            Debug.Log("Picked up " + itemId.ToString());
            // Add relic to Inventory
            GameManager.Instance.gameData.relicInInventory = true;
            item.DisableInteract();
            ObjectPoolManager.ReturnObjectToPool(item.gameObject);
            return;
        }


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
            ObjectPoolManager.ReturnObjectToPool(item.gameObject);
        }
    }


    private void DropItem()
    {
        // Check if player has item in hand
        if (GameManager.Instance.gameData.itemInHand == Globals.ItemIndex.None) return;

        Globals.ItemIndex itemId = GameManager.Instance.gameData.itemInHand;
        Debug.Log("Dropped " + GameManager.Instance.gameData.itemInHand.ToString());

        // Drop item on ground
        GameObject itemToSpawn = Resources.Load<GameObject>("Prefabs/Items/" + itemId.ToString());
        GameManager.Instance.gameData.itemInHand = Globals.ItemIndex.None;
        Transform player = GameManager.Instance.player.transform;
        ObjectPoolManager.SpawnObject(itemToSpawn, player.position, player.rotation, ObjectPoolManager.PoolType.Items);
    }


    private void DropRelic()
    {
        // Check if player has relic
        if (!GameManager.Instance.gameData.relicInInventory) return;

        Debug.Log("Dropped Relic");
        // Drop relic on ground
        GameObject itemToSpawn = Resources.Load<GameObject>("Prefabs/Items/Relic");
        GameManager.Instance.gameData.relicInInventory = false;
        Transform player = GameManager.Instance.player.transform;
        ObjectPoolManager.SpawnObject(itemToSpawn, player.position, player.rotation, ObjectPoolManager.PoolType.Items);
    }
}
