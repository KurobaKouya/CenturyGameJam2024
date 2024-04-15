using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Globals.ItemIndex itemToSpawn = Globals.ItemIndex.None;


    private void Start()
    {
        if (itemToSpawn == Globals.ItemIndex.None) return;

        StartCoroutine(SpawnItem());
    }


    IEnumerator SpawnItem()
    {
        yield return null;

        GameObject item = Resources.Load<GameObject>("Prefabs/Items/" + itemToSpawn.ToString());
        ObjectPoolManager.SpawnObject(item, transform.position, Quaternion.Euler(Vector3.zero), ObjectPoolManager.PoolType.Items);

        gameObject.SetActive(false);
    }
}
