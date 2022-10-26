using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _travelSpeed = 25f;

    Tower _origin;
    Enemy _target;
    bool _isSpecial = false;
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _travelSpeed * Time.deltaTime);
        transform.LookAt(_target.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        Impact(collision);
    }

    public void InitializeProjectile(Tower origin, Enemy target, bool isSpecial)
    {
        _origin = origin;
        _target = target;
        _isSpecial = isSpecial;
    }

    void Impact(Collision collision)
    {
        Enemy enemyHit = collision.gameObject.GetComponent<Enemy>();
        
        if (enemyHit != null)
        {
            enemyHit.DecreaseHealth((int)Mathf.Round(_origin.Damage * _origin.DamageModifier), false);
        }
        
        Feedback();
        Destroy(this.gameObject);
    }

    void Feedback()
    {
        //
    }
}
