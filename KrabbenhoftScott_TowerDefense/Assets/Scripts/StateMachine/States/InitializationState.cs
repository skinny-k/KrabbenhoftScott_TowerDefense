using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationState : TowerDefenseState
{
    static bool gameBegun = false;
    
    public override void Enter()
    {
        Debug.Log("Entered InitializationState");
        if (gameBegun)
        {
            SceneManager.LoadScene("Battlefield");
        }
        gameBegun = true;
    }

    public override void Tick()
    {
        StateMachine.ChangeState<PlayerTurnState>();
    }

    public override void Exit()
    {
        Debug.Log("Exited InitializationState");
    }
}
