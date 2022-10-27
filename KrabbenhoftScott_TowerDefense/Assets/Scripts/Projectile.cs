using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _travelSpeed = 25f;

    protected Tower _origin;
    protected Enemy _target;
    protected bool _isSpecial = false;
    
    protected virtual void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _travelSpeed * Time.deltaTime);
        transform.LookAt(_target.transform.position);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Impact(collision);
    }

    public virtual void InitializeProjectile(Tower origin, Enemy target, bool isSpecial)
    {
        _origin = origin;
        _target = target;
        _isSpecial = isSpecial;
    }

    protected virtual void Impact(Collision collision)
    {
        Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
        
        if (enemyHit != null)
        {
            enemyHit.DecreaseHealth((int)Mathf.Round(_origin.Damage * _origin.DamageModifier), false);
        }
        
        Feedback();
        Destroy(this.gameObject);
    }

    protected virtual void Feedback()
    {
        //
    }
}
