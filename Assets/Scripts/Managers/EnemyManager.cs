using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] public List<Enemy> enemyList = new();
    [SerializeField] private GameObject enemyPrefab;
    

    private void OnEnable()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }


    private void Update()
    {
        if (!GameManager.Instance.player) return;
        SpawnEnemies();

        foreach (Enemy en in enemyList) 
        {
            if (en == null) break;
            if (en.isDead)
            {
                enemyList.Remove(en);
                ObjectPoolManager.ReturnObjectToPool(en.gameObject);
                break;
            }
            
            float distanceFromPlayer = Vector3.Distance(en.transform.position, GameManager.Instance.player.transform.position);
            if (distanceFromPlayer > Globals.maxDistanceFromPlayer) en.isDead = true;

            en.UpdateLoop();
        }
    }


    private void SpawnEnemies()
    {
        if (enemyList.Count < Globals.maxSpawnCount) SpawnEnemy();
    }


    private void SpawnEnemy()
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
        Vector3 playerPos = GameManager.Instance.player.transform.position;

        // NSEW
        float posX = 0;
        float posZ = 0;

        int direction = Random.Range(0, 4);
        switch (direction)
        {
            // North
            case 0:
                posZ = Globals.minSpawnDistance + playerPos.z;
                posX = Random.Range(-Globals.spawnVariance, Globals.spawnVariance) + playerPos.x;
                break;

            // South
            case 1:
                posZ = -Globals.minSpawnDistance + playerPos.z;
                posX = Random.Range(-Globals.spawnVariance, Globals.spawnVariance) + playerPos.x;
                break;

            // East
            case 2:
                posX = Globals.minSpawnDistance + playerPos.x;
                posZ = Random.Range(-Globals.spawnVariance, Globals.spawnVariance) + playerPos.z;
                break;

            // West
            case 3:
                posX = -Globals.minSpawnDistance + playerPos.x;
                posZ = Random.Range(-Globals.spawnVariance, Globals.spawnVariance) + playerPos.z;
                break;

            // North
            default:
                posZ = Globals.minSpawnDistance + playerPos.z;
                posX = Random.Range(-Globals.spawnVariance, Globals.spawnVariance) + playerPos.x;
                break;
        }


        return new(posX, playerPos.y, posZ);
    }
}
