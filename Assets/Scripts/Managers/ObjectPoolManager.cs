using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new();
    private static GameObject objectPoolHolder;
    private static GameObject itemPool;
    private static GameObject enemyPool;

    public enum PoolType
    {
        None,
        Items,
        Enemy,
    }
    public static PoolType poolingtype;

    private void Awake()
    {
        SetupEmpties();
    }


    private void SetupEmpties()
    {
        objectPoolHolder = new("Pooled Objects");

        itemPool = new("Items");
        itemPool.transform.SetParent(objectPoolHolder.transform);

        enemyPool = new("Enemies");
        enemyPool.transform.SetParent(objectPoolHolder.transform);
    }


    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool  = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        // Pool doesn't exist
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        
        // If there are no inactive objects available, create a new object
        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            
            GameObject parentObject = SetParentObject(poolType);
            if (parentObject != null) spawnableObj.transform.SetParent(parentObject.transform);
        }
        // Reactivate inactive object
        else
        {
            spawnableObj.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }


    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parentTransform)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        // Pool doesn't exist
        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Check if there are any inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        
        // If there are no inactive objects available, create a new object
        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, parentTransform);
        }
        // Reactivate inactive object
        else
        {
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }


    public static void ReturnObjectToPool(GameObject obj)
    {
        // Remove "(Clone)" by removing 7 characters from the name of the object
        string objName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objName);

        if (pool == null) Debug.LogWarning("Warning: Trying to release an object that is not pooled: " + obj.name);
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }


    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.Enemy: 
                return enemyPool;
            case PoolType.Items:
                return itemPool;
            case PoolType.None:
                return null;
            default: 
                return null;
        }
    }
}


public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new();
}
