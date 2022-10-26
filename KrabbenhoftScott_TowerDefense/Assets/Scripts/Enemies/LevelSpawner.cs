using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] Enemy[] _enemyPrefabs;
    [SerializeField] float _spawnBuffer = 0.2f;

    EnemySpawner[] _enemySpawners;
    
    void OnEnable()
    {
        EnemyTurnState.OnEnemyTurnBegin += BeginTurn;
    }

    void Awake()
    {
        _enemySpawners = (EnemySpawner[])FindObjectsOfType(typeof(EnemySpawner));
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
            EnemySpawner spawner = ChooseSpawner();

            spawner.SpawnEnemy(enemyPrefab);

            yield return new WaitForSeconds(_spawnBuffer);
        }
    }

    EnemySpawner ChooseSpawner()
    {
        return _enemySpawners[(int)Random.Range(0, _enemySpawners.Length)];
    }

    void OnDisable()
    {
        EnemyTurnState.OnEnemyTurnBegin -= BeginTurn;
    }
}
