using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicAltar : Interactable
{
    public override void Interact()
    {
        if (!GameManager.Instance.gameData.relicInInventory)
        {
            Debug.Log("No relic in inventory!");
            return;
        }

        GameManager.Instance.gameData.relicsCollected += 1;
        GameManager.Instance.gameData.relicInInventory = false;

        if (GameManager.Instance.gameData.relicsCollected >= 5) GameEvents.Instance.AllRelicsCollected();
    }
}
