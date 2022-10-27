using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(Enemy enemyPrefab)
    {
        Vector3 spawnAt = transform.GetChild(Random.Range(0, transform.childCount)).position;
        EnemyTurnState.EnemiesInScene.Add(Instantiate(enemyPrefab, spawnAt, Quaternion.identity));
    }
}
