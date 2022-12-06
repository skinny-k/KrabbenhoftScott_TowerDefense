using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTower : Tower
{
    [Header("Splash Tower Settings")]
    [SerializeField] Transform _projectileOrigin;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] float _explosiveRadius = 2.5f;
    [SerializeField] int _explosiveDamage = 5;

    [Header("Upgrade Settings")]
    [SerializeField] float _rangeIncrease = 2.5f;
    [SerializeField] float _fireRateDecrease = 0.1f;
    [SerializeField] int _damageIncrease = 2;
    [SerializeField] float _explosiveRadiusIncrease = 2;
    [SerializeField] int _explosiveDamageIncrease = 2;

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
            AudioHelper.PlayClip2D(_fireSFX, _volume);
        }

        _target = FindTarget();
        UpdateLookTarget();
        
        StartCoroutine(FireCooldown());
    }

    public override void UpgradeTower()
    {
        _levelChevrons[_level].SetActive(true);
        _level++;

        _range += _rangeIncrease;
        _fireRate -= _fireRateDecrease;
        _damage += _damageIncrease;
        _explosiveRadius += _explosiveRadiusIncrease;
        _explosiveDamage += _explosiveDamageIncrease;
    }
}
