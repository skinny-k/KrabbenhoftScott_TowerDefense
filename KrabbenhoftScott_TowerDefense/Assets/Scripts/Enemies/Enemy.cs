using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected abstract void Move();
    public abstract int GetSpawnCountOfTurn(int turn);

    [Header("Enemy Settings")]
    [SerializeField] protected float _moveSpeed = 5f;
    [SerializeField] protected int _contactDamage = 10;

    Health _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }
    
    void FixedUpdate()
    {
        Move();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.DecreaseHealth(_contactDamage, false);
        }
    }
    
    public void DecreaseHealth(int damage, bool isSpecial)
    {
        _health.DecreaseHealth(damage, isSpecial);
    }

    public void OnDisable()
    {
        EnemyTurnState.EnemiesInScene.Remove(this);
    }
}
