using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTower : Tower
{
    [SerializeField] Transform _projectileOrigin;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] float _explosiveRadius = 2.5f;
    [SerializeField] int _explosiveDamage = 5;

    public float ExplosiveRadius
    {
        get => _explosiveRadius;
        set => _explosiveRadius = value;
    }
    public int ExplosiveDamage
    {
        get => _explosiveDamage;
        set => _explosiveDamage = value;
    }
    
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
        }

        _target = FindTarget();
        UpdateLookTarget();
        
        StartCoroutine(FireCooldown());
    }

    public override void UpgradeTower()
    {
        _level++;
    }
}
