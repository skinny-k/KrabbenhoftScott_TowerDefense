using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationState : TowerDefenseState
{
    bool _gameBegan = false;
    bool _sceneChanging = false;
    
    public override void Enter()
    {
        Debug.Log("Entered InitializationState");

        if (_gameBegan)
        {
            Debug.Log("Reloading scene...");
            _sceneChanging = true;
            EnemyTurnState.Turn = 0;
            SceneManager.LoadScene("Battlefield");
        }
        _gameBegan = true;
    }

    public override void Tick()
    {
        if (!_sceneChanging)
        {
            StateMachine.ChangeState<PlayerTurnState>();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exited InitializationState");
    }
}
