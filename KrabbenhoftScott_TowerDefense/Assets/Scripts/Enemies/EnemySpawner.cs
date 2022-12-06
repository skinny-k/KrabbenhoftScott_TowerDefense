using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnNodes;
    
    public void SpawnEnemy(EnemyPool enemyPool)
    {
        Vector3 spawnAt = _spawnNodes[Random.Range(0, _spawnNodes.Length)].position;
        Enemy enemySpawned = enemyPool.ActivateFromPool();
        enemySpawned.transform.position = spawnAt;
    }
}
