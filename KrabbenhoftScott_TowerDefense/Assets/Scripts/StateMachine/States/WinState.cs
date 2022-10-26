using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : TowerDefenseState
{
    public static event Action OnWinStateEnter;
    public static event Action OnWinStateExit;
    
    public override void Enter()
    {
        Debug.Log("Entered WinState");
        OnWinStateEnter?.Invoke();
        SubscribeToInput();
    }

    public override void Exit()
    {
        Debug.Log("Exited WinState");
        OnWinStateExit?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        // InputController.Instance.OnPausePress += Method;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        // InputController.Instance.OnPausePress -= Method;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
    }
}
