using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] KeyCode _passTurnKey = KeyCode.Q;
    [SerializeField] KeyCode _interactKey = KeyCode.Space;
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField] KeyCode _muteKey = KeyCode.M;
    [SerializeField] KeyCode _modifierKey = KeyCode.LeftShift;
    [SerializeField] KeyCode _altModifierKey = KeyCode.RightShift;
    
    public static event Action<Vector3> OnLMBPress;
    public static event Action OnQuitPress;
    public static event Action OnPassTurnPress;
    public static event Action OnInteractPress;
    public static event Action OnPausePress;
    public static event Action OnMutePress;
    public static event Action OnAnyKeyPress;

    public static event Action OnGameContinue;

    void Update()
    {
        CheckQuit();
        CheckLMB();
        CheckPassTurn();
        CheckInteract();
        CheckPause();
        CheckMute();
        CheckAnyKey();
    }

    void CheckQuit()
    {
        if (Input.GetKeyDown(_pauseKey) && (Input.GetKeyDown(_modifierKey) || Input.GetKeyDown(_altModifierKey)))
        {
            OnQuitPress?.Invoke();
        }
    }
    
    void CheckLMB()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLMBPress?.Invoke(Input.mousePosition);
        }
    }
    
    void CheckPassTurn()
    {
        if (Input.GetKeyDown(_passTurnKey))
        {
            OnPassTurnPress?.Invoke();
        }
    }

    void CheckInteract()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            OnInteractPress?.Invoke();
        }
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            OnPausePress?.Invoke();
        }
    }

    void CheckMute()
    {
        if (Input.GetKeyDown(_muteKey))
        {
            OnMutePress?.Invoke();
        }
    }

    void CheckAnyKey()
    {
        bool mousePressed = false;
        
        for (int i = 0; i < 7; i++)
        {
            mousePressed = mousePressed || Input.GetMouseButtonDown(i);
        }
        
        if (Input.anyKeyDown && !mousePressed)
        {
            OnAnyKeyPress?.Invoke();
        }
    }

    public void InvokePausePress()
    {
        OnPausePress?.Invoke();
    }

    public void InvokePassTurnPress()
    {
        OnPassTurnPress?.Invoke();
    }

    public void InvokeGameContinue()
    {
        OnGameContinue?.Invoke();
    }
}
