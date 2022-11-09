using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnNodes;
    
    public void SpawnEnemy(Enemy enemyPrefab)
    {
        Vector3 spawnAt = _spawnNodes[Random.Range(0, _spawnNodes.Length)].position;
        EnemyTurnState.EnemiesInScene.Add(Instantiate(enemyPrefab, spawnAt, Quaternion.identity));
    }
}
