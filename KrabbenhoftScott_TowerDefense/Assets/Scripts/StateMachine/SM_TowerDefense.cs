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
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();
    }
    
    void OnEnable()
    {
        InputController.Instance.OnQuitPress += QuitGame;
    }
    
    void Start()
    {
        ChangeState<InitializationState>();
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void OnDisable()
    {
        InputController.Instance.OnQuitPress -= QuitGame;
    }
}
