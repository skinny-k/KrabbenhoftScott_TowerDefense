using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] float _radius = 2f;

    SplashTower _splashOrigin;

    protected override void OnCollisionEnter(Collision collision)
    {
        Impact(collision);
    }

    public void InitializeProjectile(SplashTower origin, Enemy target, bool isSpecial)
    {
        _splashOrigin = origin;
        _targetEnemy = target;
        _isSpecial = isSpecial;

        _targetEnemy.OnEnemyDisable += AdjustForTargetDeath;
    }

    protected override void Impact(Collision collision)
    {
        Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
        
        if (enemyHit != null)
        {
            enemyHit.DecreaseHealth((int)Mathf.Round(_splashOrigin.Damage * _splashOrigin.DamageModifier), false);
        }

        Explode();
        
        Feedback();
        Destroy(this.gameObject);
    }

    void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, transform.forward, Mathf.Infinity, LayerMask.NameToLayer("Enemies"));
        Enemy[] enemies = new Enemy[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            enemies[i] = hits[i].collider.gameObject.GetComponent<Enemy>();
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.DecreaseHealth((int)Mathf.Round(_splashOrigin.ExplosiveDamage * _splashOrigin.DamageModifier), false);
        }
    }

    protected override void Feedback()
    {
        //
    }
}
