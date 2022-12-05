using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    protected override void ResetObjectToDefaults(Enemy enemyToReset)
    {
        enemyToReset.Initialize(this);
    }

    public override void ReturnToPool(Enemy enemyToReturn)
    {
        EnemyTurnState.EnemiesInScene.Remove(enemyToReturn);
        
        base.ReturnToPool(enemyToReturn);
    }
}
