using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] int _startingBalance = 100;

    public static event Action<int> OnBalanceUpdate;
    
    int _balance = 0;

    public int Balance
    {
        get => _balance;
    }
    
    void Awake()
    {
        Deposit(_startingBalance);
    }
    
    public void Deposit(int deposit)
    {
        _balance += deposit;
        OnBalanceUpdate?.Invoke(_balance);
    }

    public void Withdraw(int withdrawal)
    {
        _balance -= withdrawal;
        OnBalanceUpdate?.Invoke(_balance);
    }
}
