using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UIBase
{
    [SerializeField] private Image item1Image = null;
    [SerializeField] private Image item2Image = null;


    private void OnEnable()
    {
        UIEvents.onToggleInventory += Toggle;
    }


    private void OnDisable()
    {
        UIEvents.onToggleInventory -= Toggle;
    }


    public override void Toggle()
    {
        base.Toggle();

        // Setup/Update Inventory
        // TODO
        // ...
        // SetActive images if items are not null in Inventory System
        // Obtain sprite from resources
        // Drag & Drop to drop items?
        // Update Inventory System using events + drop item in world space

    }
}
