using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    IState _currentState;
    protected IState _previousState;

    public IState CurrentState => _currentState;
    protected bool InTransition
    {
        get;
        private set;
    }

    protected virtual void Awake()
    {
        InTransition = false;
    }
    
    public void ChangeState<T>() where T : IState
    {
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.LogWarning("State does not exist on state machine object!");
            return;
        }
        else
        {
            InitiateStateChange(targetState);
        }
    }

    public void RevertState()
    {
        if (_previousState != null)
        {
            InitiateStateChange(_previousState);
        }
    }

    void InitiateStateChange(IState targetState)
    {
        if (targetState != _currentState && !InTransition)
        {
            Transition(targetState);
        }
    }

    void Transition(IState newState)
    {
        InTransition = true;
        _currentState?.Exit();
        _previousState = _currentState;
        _currentState = newState;
        _currentState?.Enter();
        InTransition = false;
    }

    protected virtual void Update()
    {
        if (_currentState != null && !InTransition)
        {
            _currentState.Tick();
        }
    }
}
