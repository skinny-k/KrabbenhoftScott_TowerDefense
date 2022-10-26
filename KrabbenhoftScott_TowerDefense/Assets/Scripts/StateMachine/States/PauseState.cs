using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : TowerDefenseState
{
    public static event Action OnPause;
    public static event Action OnUnpause;
    
    bool _active = false;
    
    public override void Enter()
    {
        Debug.Log("Entered PauseState");
        Time.timeScale = 0;
        OnPause?.Invoke();
        SubscribeToInput();
    }

    public override void Exit()
    {
        Debug.Log("Exited PauseState");
        Time.timeScale = 1;
        OnUnpause?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        InputController.Instance.OnPausePress += Unpause;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.Instance.OnPausePress -= Unpause;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
    }

    void Unpause()
    {
        StateMachine.RevertState();
    }
}
