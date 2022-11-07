using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] int _startingBalance = 100;
    
    int _balance = 0;
    
    void Awake()
    {
        _balance = _startingBalance;
    }
    
    public void Deposit(int deposit)
    {
        _balance += deposit;
        Debug.Log("Remaining resources: " + _balance);
    }

    public void Withdraw(int withdrawal)
    {
        _balance -= withdrawal;
        Debug.Log("Remaining resources: " + _balance);
    }
}
