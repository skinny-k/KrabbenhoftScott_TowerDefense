using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance
    {
        get;
        private set;
    }

    [SerializeField] KeyCode _passTurnKey = KeyCode.Q;
    [SerializeField] KeyCode _interactKey = KeyCode.Space;
    [SerializeField] KeyCode _pauseKey = KeyCode.Escape;
    [SerializeField] KeyCode _muteKey = KeyCode.M;
    [SerializeField] KeyCode _modifierKey = KeyCode.LeftShift;
    [SerializeField] KeyCode _altModifierKey = KeyCode.RightShift;
    
    public event Action<Vector3> OnLMBPress;
    public event Action OnQuitPress;
    public event Action OnPassTurnPress;
    public event Action OnInteractPress;
    public event Action OnPausePress;
    public event Action OnMutePress;
    public event Action OnAnyKeyPress;

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
    }

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
}
