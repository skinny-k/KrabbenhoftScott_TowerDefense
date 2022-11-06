using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : TowerDefenseState
{
    public static ArrayList EnemiesInScene = new ArrayList();
    public static bool _gameContinued;

    float _timeThisTurn;
    
    public static int Turn
    {
        get;
        private set;
    }

    public static event Action OnEnemyTurnBegin;
    public static event Action OnEnemyTurnEnd;
    
    protected override void Awake()
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

        if (FindObjectsOfType(typeof(Player)).Length == 0)
        {
            StateMachine.ChangeState<LoseState>();
        }
        
        if (EnemiesInScene.Count == 0)
        {
            if (FindObjectsOfType(typeof(Enemy)).Length == 0)
            {
                StartCoroutine(PassTurn());
            }
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

        if (Turn < 5 || _gameContinued)
        {
            StateMachine.ChangeState<PlayerTurnState>();
        }
        else if (Turn >= 5)
        {
            StateMachine.ChangeState<WinState>();
        }
    }
    void Pause()
    {
        StateMachine.ChangeState<PauseState>();
    }
}
