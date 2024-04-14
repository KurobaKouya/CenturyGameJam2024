using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolScript : MonoBehaviour
{



    [SerializeField] GameObject toPool;
    [SerializeField] int poolSize;
    public List<GameObject> objectPool = new List<GameObject>();
    


   

    private void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(toPool,transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject InstantiateObject(Vector3 pos)
    {
        GameObject gameObject = GetFromPool();
        if (gameObject == null)
        {
            return null;
        }
        gameObject.SetActive(true);
        gameObject.transform.position = pos;
        return gameObject;
    }

    public void DestroyObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    GameObject GetFromPool()
    {
        foreach (var item in objectPool)
        {
            if (!item.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }


    public List<GameObject> GetActivePool()
    {
        List<GameObject> pool = new List<GameObject>();
        foreach (var item in objectPool)
        {
            if (item.activeInHierarchy)
                pool.Add(item);
        }
        return pool;
    }







}
