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
        enemyToReturn.transform.localScale = new Vector3(1, 1, 1);
        
        base.ReturnToPool(enemyToReturn);
    }

    public bool HasActiveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }
}
