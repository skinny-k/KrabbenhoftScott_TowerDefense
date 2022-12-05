using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenuState : TowerDefenseState
{
    bool _active = false;
    
    public static event Action OnTowerMenuOpen;
    public static event Action OnTowerMenuClose;

    public bool Active
    {
        get => _active;
    }
    
    public override void Enter()
    {
        Debug.Log("Entered TowerMenuState");
        OnTowerMenuOpen?.Invoke();
        TowerMenuUI.UpdateMenuContent();
        TowerMenuUI.OpenMenu();
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
        TowerPlot.OnTowerUpgrade += TowerUpgraded;
        TowerPlot.OnTowerDestroy += TowerDestroyed;
        
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        InputController.OnPausePress += Pause;
        // InputController.Instance.OnMutePress += Method;
        InputController.OnAnyKeyPress += KeyPressed;
    }

    void UnsubscribeToInput()
    {
        TowerPlot.OnTowerBuild -= TowerBuilt;
        
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.OnPausePress -= Pause;
        // InputController.Instance.OnMutePress -= Method;
        InputController.OnAnyKeyPress -= KeyPressed;
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

    void TowerUpgraded(Tower tower)
    {
        ReturnToPlayer();
    }

    void TowerDestroyed()
    {
        ReturnToPlayer();
    }

    void ReturnToPlayer()
    {
        StateMachine.ChangeState<PlayerTurnState>();
    }
}
