using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTower : Tower
{
    [SerializeField] Transform _projectileOrigin;
    [SerializeField] Projectile _projectilePrefab;
    
    protected override void Fire()
    {
        if (_target == null)
        {
            _target = FindTarget();
        }
        
        if (_target != null)
        {
            Projectile projectile = Instantiate(_projectilePrefab, _projectileOrigin.position, _projectileOrigin.rotation);
            projectile.InitializeProjectile(this, _target, false);
            // _target.DecreaseHealth((int)Mathf.Round(_damage * _damageModifier), false);
        }

        _target = FindTarget();
        UpdateLookTarget();
        
        StartCoroutine(FireCooldown());
    }
}
