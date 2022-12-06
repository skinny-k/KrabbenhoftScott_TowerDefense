using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SM_TowerDefense))]
public abstract class TowerDefenseState : MonoBehaviour, IState
{
    protected bool subscribed = false;
    
    protected SM_TowerDefense StateMachine
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        Debug.Log(gameObject.GetInstanceID());
        StateMachine = GetComponent<SM_TowerDefense>();
    }

    public virtual void Enter()
    {
        //
    }

    public virtual void Tick()
    {
        //
    }

    public virtual void FixedTick()
    {
        //
    }

    public virtual void Exit()
    {
        //
    }
}
