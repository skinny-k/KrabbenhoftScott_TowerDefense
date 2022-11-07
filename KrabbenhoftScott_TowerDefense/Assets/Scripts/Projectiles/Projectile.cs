using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _travelSpeed = 25f;

    protected Tower _origin;
    protected Enemy _targetEnemy;
    protected Vector3 _targetPosition;
    protected bool _isSpecial = false;
    
    protected virtual void Update()
    {
        Vector3 target;
        
        if (_targetEnemy != null)
        {
            target = _targetEnemy.transform.position;
        }
        else
        {
            target = _targetPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, _travelSpeed * Time.deltaTime);
        transform.LookAt(target);

        if (_targetEnemy == null && transform.position == _targetPosition)
        {
            Feedback();
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Impact(collision);
    }

    public virtual void InitializeProjectile(Tower origin, Enemy target, bool isSpecial)
    {
        _origin = origin;
        _targetEnemy = target;
        _isSpecial = isSpecial;

        _targetEnemy.OnEnemyDisable += AdjustForTargetDeath;
    }

    protected virtual void Impact(Collision collision)
    {
        _targetEnemy.OnEnemyDisable -= AdjustForTargetDeath;
        
        Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
        
        if (enemyHit != null)
        {
            enemyHit.DecreaseHealth((int)Mathf.Round(_origin.Damage * _origin.DamageModifier), false);
        }
        
        Feedback();
        Destroy(this.gameObject);
    }

    protected virtual void AdjustForTargetDeath(Vector3 newTarget)
    {
        _targetEnemy = null;
        _targetPosition = newTarget;
    }

    protected virtual void Feedback()
    {
        //
    }
}
