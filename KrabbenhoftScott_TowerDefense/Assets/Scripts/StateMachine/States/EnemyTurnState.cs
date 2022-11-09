using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : TowerDefenseState
{
    public static ArrayList EnemiesInScene = new ArrayList();
    public static ArrayList PlayersInScene = new ArrayList();
    
    public static int Turn;

    public static event Action OnEnemyTurnBegin;
    public static event Action OnEnemyTurnEnd;

    float _timeThisTurn;
    
    public override void Awake()
    {
        Turn = 0;
        base.Awake();
    }
    
    public override void Enter()
    {
        Debug.Log("Entered EnemyTurnState");
        Turn++;
        _timeThisTurn = 0f;
        OnEnemyTurnBegin?.Invoke();
        SubscribeToInput();
    }

    public override void Tick()
    {
        _timeThisTurn += Time.deltaTime;

        if (PlayersInScene.Count == 0 && _timeThisTurn >= 2f)
        {
            StateMachine.ChangeState<LoseState>();
        }
        
        if (EnemiesInScene.Count == 0 && _timeThisTurn >= 2f)
        {
            StartCoroutine(PassTurn());
        }
    }

    public override void Exit()
    {
        Debug.Log("Exited EnemyTurnState");
        OnEnemyTurnEnd?.Invoke();
        UnsubscribeToInput();
    }

    void SubscribeToInput()
    {
        // InputController.Instance.OnLMBPress += Method;
        // InputController.Instance.OnPassTurnPress += Method;
        // InputController.Instance.OnInteractPress += Method;
        InputController.Instance.OnPausePress += Pause;
        // InputController.Instance.OnMutePress += Method;
        // InputController.Instance.OnAnyKeyPress += Method;
    }

    void UnsubscribeToInput()
    {
        // InputController.Instance.OnLMBPress -= Method;
        // InputController.Instance.OnPassTurnPress -= Method;
        // InputController.Instance.OnInteractPress -= Method;
        InputController.Instance.OnPausePress -= Pause;
        // InputController.Instance.OnMutePress -= Method;
        // InputController.Instance.OnAnyKeyPress -= Method;
    }

    IEnumerator PassTurn()
    {
        yield return new WaitForSeconds(1f);

        if (Turn == 5)
        {
            StateMachine.ChangeState<WinState>();
        }
        else if (Turn != 5)
        {
            StateMachine.ChangeState<PlayerTurnState>();
        }
    }
    void Pause()
    {
        StateMachine.ChangeState<PauseState>();
    }
}
