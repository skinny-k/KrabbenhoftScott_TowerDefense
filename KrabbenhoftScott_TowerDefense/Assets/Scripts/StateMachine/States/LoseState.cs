using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : TowerDefenseState
{
    public static event Action OnLoseStateEnter;
    public static event Action OnLoseStateExit;
    
    public override void Enter()
    {
        Debug.Log("Entered LoseState");
        OnLoseStateEnter?.Invoke();
        SubscribeToInput();
    }

    public override void Exit()
    {
        Debug.Log("Exited LoseState");
        OnLoseStateExit?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        InputController.Instance.OnGameRestart += RestartGame;
        InputController.Instance.OnGameQuit += QuitGame;
        
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        // InputController.Instance.OnPausePress += Method;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        InputController.Instance.OnGameRestart -= RestartGame;
        InputController.Instance.OnGameQuit -= QuitGame;
        
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        // InputController.Instance.OnPausePress -= Method;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
    }

    void RestartGame()
    {
        Debug.Log("Restarting game...");
        StateMachine.ChangeState<InitializationState>();
    }
    
    void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
