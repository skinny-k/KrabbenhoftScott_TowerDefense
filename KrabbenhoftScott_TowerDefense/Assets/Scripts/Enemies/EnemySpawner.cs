using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] Enemy[] _enemyPrefabs;

    /*
    void OnEnable()
    {
        EnemyTurnState.OnEnemyTurnBegin += BeginTurn;
    }

    void BeginTurn()
    {
        Debug.Log("Spawn enemies for turn " + EnemyTurnState.Turn);

        foreach (Enemy enemyType in _enemyPrefabs)
        {
            StartCoroutine(SpawnEnemies(enemyType, enemyType.GetSpawnCountOfTurn(EnemyTurnState.Turn)));
        }
    }
    
    IEnumerator SpawnEnemies(Enemy enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnAt = transform.GetChild(Random.Range(0, transform.childCount)).position;
            Instantiate(enemyPrefab, spawnAt, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void SpawnEnemies(Enemy enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(enemyPrefab, count);
        }
    }
    */

    public void SpawnEnemy(Enemy enemyPrefab)
    {
        Vector3 spawnAt = transform.GetChild(Random.Range(0, transform.childCount)).position;
        EnemyTurnState.EnemiesInScene.Add(Instantiate(enemyPrefab, spawnAt, Quaternion.identity));
    }

    /*
    void OnDisable()
    {
        EnemyTurnState.OnEnemyTurnBegin -= BeginTurn;
    }
    */
}
