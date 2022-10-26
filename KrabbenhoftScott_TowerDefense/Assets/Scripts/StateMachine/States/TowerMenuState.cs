using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenuState : TowerDefenseState
{
    //static TowerPlot _currentPlot;
    bool _active = false;
    
    public static event Action OnTowerMenuOpen;
    public static event Action OnTowerMenuClose;

    /*
    public static TowerPlot CurrentPlot
    {
        get => _currentPlot;
        set => _currentPlot = value;
    }
    */
    
    public override void Enter()
    {
        Debug.Log("Entered TowerMenuState");
        TowerMenuUI.OpenMenu();
        OnTowerMenuOpen?.Invoke();
        SubscribeToInput();
    }

    public override void Tick()
    {
        _active = true;

        TowerMenuUI.UpdateMenuPlacement();
    }

    public override void Exit()
    {
        Debug.Log("Exited TowerMenuState");
        _active = false;
        TowerMenuUI.CloseMenu();
        OnTowerMenuClose?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        TowerPlot.OnTowerBuild += TowerBuilt;
        
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        InputController.Instance.OnPausePress += Pause;
        // InputController.Instance.OnMutePress += Method;
        InputController.Instance.OnAnyKeyPress += KeyPressed;
    }

    void UnsubscribeToInput()
    {
        TowerPlot.OnTowerBuild -= TowerBuilt;
        
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.Instance.OnPausePress -= Pause;
        // InputController.Instance.OnMutePress -= Method;
        InputController.Instance.OnAnyKeyPress -= KeyPressed;
    }

    void Pause()
    {
        StateMachine.ChangeState<PauseState>();
    }

    void KeyPressed()
    {
        if (_active)
        {
            ReturnToPlayer();
        }
    }

    void TowerBuilt(Tower tower)
    {
        ReturnToPlayer();
    }

    void ReturnToPlayer()
    {
        StateMachine.ChangeState<PlayerTurnState>();
    }
}
