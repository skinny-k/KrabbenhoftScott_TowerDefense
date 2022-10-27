using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _wrench;
    
    Health _health;
    PlayerMovement _movement;

    public Health MyHealth
    {
        get => _health;
    }
    public PlayerMovement MyMovement
    {
        get => _movement;
    }

    void OnEnable()
    {
        PlayerTurnState.OnPlayerTurnBegin += BeginTurn;
        PlayerTurnState.OnPlayerTurnEnd += EndTurn;

        EnemyTurnState.OnEnemyTurnBegin += DisableWrench;
    }
    
    void Awake()
    {
        _health = GetComponent<Health>();
        _movement = GetComponent<PlayerMovement>();
    }

    void BeginTurn()
    {
        EnableWrench();
    }

    void EndTurn()
    {
        //
    }
    
    void EnableWrench()
    {
        _wrench.SetActive(true);
    }

    void DisableWrench()
    {
        _wrench.SetActive(false);
    }

    public void ModifySpeed(float moveModifier, float duration)
    {
        StartCoroutine(_movement.ModifySpeed(moveModifier, duration));
    }
    
    public void DecreaseHealth(int damage, bool isSpecial)
    {
        _health.DecreaseHealth(damage, isSpecial);
    }
    
    public void IncreaseHealth(int heal)
    {
        _health.IncreaseHealth(heal);
    }

    void OnDisable()
    {
        PlayerTurnState.OnPlayerTurnBegin -= BeginTurn;
        PlayerTurnState.OnPlayerTurnEnd -= EndTurn;

        EnemyTurnState.OnEnemyTurnBegin -= DisableWrench;
    }
}
