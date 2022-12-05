using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] ParticleSystem _explodeParticles;
    [SerializeField] AudioClip _explodeSFX;
    [SerializeField] float _volume = 1;
    
    SplashTower _originAsSplashTower;
    
    public override void InitializeProjectile(Tower origin, Enemy target, bool isSpecial)
    {
        _origin = origin;
        _originAsSplashTower = _origin.GetComponent<SplashTower>();
        _targetEnemy = target;
        _isSpecial = isSpecial;

        _targetEnemy.OnEnemyDisable += AdjustForTargetDeath;
    }

    protected override void Impact(Collision collision)
    {
        Explode();

        base.Impact(collision);
    }

    protected virtual void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _originAsSplashTower.ExplosiveRadius, Vector3.forward);
        ArrayList enemies = new ArrayList();

        foreach (RaycastHit hit in hits)
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.DecreaseHealth(_originAsSplashTower.ExplosiveDamage, false);
        }

        if (_explodeParticles != null)
        {
            Instantiate(_explodeParticles, transform.position, Quaternion.identity);
        }
        if (_explodeSFX != null)
        {
            AudioHelper.PlayClip3D(_explodeSFX, _volume, transform.position);
        }
    }
}
