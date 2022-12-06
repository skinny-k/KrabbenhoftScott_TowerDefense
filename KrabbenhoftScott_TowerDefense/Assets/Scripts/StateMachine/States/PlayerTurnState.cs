using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : TowerDefenseState
{
    public static event Action OnPlayerTurnBegin;
    public static event Action OnPlayerTurnEnd;
    public static bool InPlayerTurn = false;
    
    public override void Enter()
    {
        Debug.Log("Entered PlayerTurnState");
        InPlayerTurn = true;
        OnPlayerTurnBegin?.Invoke();
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
        Debug.Log("Exited PlayerTurnState");
        InPlayerTurn = false;
        OnPlayerTurnEnd?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        TowerPlot.OnPlotClick += OpenTowerMenu;
        
        // InputController.Instance.OnLMBPress += Method;
        InputController.OnPassTurnPress += PassTurn;
        // InputController.Instance.OnInteractPress += Method;
        InputController.OnPausePress += Pause;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;

        subscribed = true;
    }

    void UnsubscribeToInput()
    {
        TowerPlot.OnPlotClick -= OpenTowerMenu;
        
        // InputController.Instance.OnLMBPress -= Method;
        InputController.OnPassTurnPress -= PassTurn;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.OnPausePress -= Pause;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;

        subscribed = false;
    }

    void OpenTowerMenu(TowerPlot plot)
    {
        StateMachine.ChangeState<TowerMenuState>();
        TowerMenuUI.Instance.CurrentPlot = plot;
    }
    
    void PassTurn()
    {
        StateMachine.ChangeState<EnemyTurnState>();
    }

    void Pause()
    {
        StateMachine.ChangeState<PauseState>();
    }
}
