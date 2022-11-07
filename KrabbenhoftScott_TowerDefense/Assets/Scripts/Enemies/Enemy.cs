using System;
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
    [SerializeField] protected int _rewardAmount = 5;

    protected Health _health;
    protected bool _isGrounded = false;

    public event Action<Vector3> OnEnemyDisable;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();

        _health.OnDie += OnDie;
    }
    
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.DecreaseHealth(_contactDamage, false);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
    
    public virtual void DecreaseHealth(int damage, bool isSpecial)
    {
        _health.DecreaseHealth(damage, isSpecial);
    }

    protected virtual void OnDie()
    {
        Player.DepositFunds(_rewardAmount);
    }

    protected virtual void OnDisable()
    {
        EnemyTurnState.EnemiesInScene.Remove(this);
        OnEnemyDisable?.Invoke(transform.position);
    }
}
