using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_TowerDefense : StateMachineBase
{
    public static SM_TowerDefense Instance
    {
        get;
        private set;
    }
    
    protected virtual void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InputController.OnQuitPress += QuitGame;
    }
    
    protected virtual void Start()
    {
        ChangeState<InitializationState>();
    }

    protected virtual void QuitGame()
    {
        Application.Quit();
    }

    protected virtual void OnDisable()
    {
        InputController.OnQuitPress -= QuitGame;
    }
    
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
