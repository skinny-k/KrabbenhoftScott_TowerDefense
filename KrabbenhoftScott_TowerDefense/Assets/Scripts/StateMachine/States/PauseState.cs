using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseState : TowerDefenseState
{
    public static event Action OnPause;
    public static event Action OnUnpause;
    public static bool Paused = false;
    
    public override void Enter()
    {
        Debug.Log("Entered PauseState");
        Time.timeScale = 0;
        Paused = true;
        OnPause?.Invoke();
        // SubscribeToInput();
    }

    public override void Tick()
    {
        if (!subscribed)
        {
            SubscribeToInput();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exited PauseState");
        Time.timeScale = 1;
        Paused = false;
        OnUnpause?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        InputController.OnPausePress += Unpause;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.OnPausePress -= Unpause;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
    }

    void Unpause()
    {
        StateMachine.RevertState();
    }
}
