using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTower : Tower
{
    [Header("Special Tower Settings")]
    [SerializeField] Transform _projectileOrigin;
    [SerializeField] Projectile _projectilePrefab;

    [Header("Upgrade Settings")]
    [SerializeField] float _rangeIncrease = 2.5f;
    [SerializeField] float _fireRateDecrease = 0.1f;
    [SerializeField] int _damageIncrease = 2;
    
    protected override void Fire()
    {
        if (_target == null)
        {
            _target = FindTarget();
        }
        
        if (_target != null)
        {
            Projectile projectile = Instantiate(_projectilePrefab, _projectileOrigin.position, _projectileOrigin.rotation);
            projectile.InitializeProjectile(this, _target, true);
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
    }
}
