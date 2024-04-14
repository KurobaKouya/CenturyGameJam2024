using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : Singleton<EnemyManager>
{
    // private GameObject
    private List<Enemy> enemyList = new();
    private GameObject enemyPrefab;


    public void Init()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }


    public void UpdateLoop()
    {
        if (Input.GetKeyDown(KeyCode.J)) SpawnEnemy();

        foreach (Enemy en in enemyList) 
        {
            if (en.isDead)
            {
                enemyList.Remove(en);
                ObjectPoolManager.ReturnObjectToPool(en.gameObject);
                break;
            }


            en.UpdateLoop();
        }
    }


    public void SpawnEnemy()
    {
        Vector3 spawnPosition = FindSpawnPosition();
        Quaternion SpawnRotation = Quaternion.Euler(Vector3.zero);
        GameObject enemyObj = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPosition, SpawnRotation, ObjectPoolManager.PoolType.Enemy);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.Init();
        enemyList.Add(enemy);
    }


    private Vector3 FindSpawnPosition()
    {
        Vector3 position = Vector3.zero;


        return position;
    }
}
