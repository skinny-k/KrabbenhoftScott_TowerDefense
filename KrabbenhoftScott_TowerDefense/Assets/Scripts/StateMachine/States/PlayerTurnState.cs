using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : TowerDefenseState
{
    public static event Action OnPlayerTurnBegin;
    public static event Action OnPlayerTurnEnd;
    
    bool _active = false;
    
    public override void Enter()
    {
        Debug.Log("Entered PlayerTurnState");
        OnPlayerTurnBegin?.Invoke();
        SubscribeToInput();
    }

    public override void Exit()
    {
        Debug.Log("Exited PlayerTurnState");
        OnPlayerTurnEnd?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        TowerPlot.OnPlotClick += OpenTowerMenu;
        
        // InputController.Instance.OnLMBPress += Method;
        InputController.Instance.OnPassTurnPress += PassTurn;
        // InputController.Instance.OnInteractPress += Method;
        InputController.Instance.OnPausePress += Pause;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        TowerPlot.OnPlotClick -= OpenTowerMenu;
        
        // InputController.Instance.OnLMBPress -= Method;
        InputController.Instance.OnPassTurnPress -= PassTurn;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.Instance.OnPausePress -= Pause;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
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
