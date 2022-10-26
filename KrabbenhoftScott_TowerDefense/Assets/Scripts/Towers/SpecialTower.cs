using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTower : Tower
{
    protected override void Fire()
    {
        if (_target == null)
        {
            _target = FindTarget();
        }
        else
        {
            _target.DecreaseHealth((int)Mathf.Round(_damage * _damageModifier), true);
        }

        _target = FindTarget();
        UpdateLookTarget();
        
        StartCoroutine(FireCooldown());
    }
}
