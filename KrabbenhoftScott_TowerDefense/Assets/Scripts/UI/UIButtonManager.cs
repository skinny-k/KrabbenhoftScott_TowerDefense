using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _passTurnButton;
    [SerializeField] SM_TowerDefense StateMachine;

    void OnEnable()
    {
        PauseState.OnPause += UpdatePauseMenus;
        PauseState.OnUnpause += UpdatePauseMenus;

        PlayerTurnState.OnPlayerTurnBegin += UpdatePassTurnButton;
        PlayerTurnState.OnPlayerTurnEnd += UpdatePassTurnButton;
    }

    void UpdatePauseMenus()
    {
        _pauseMenu.SetActive(PauseState.Paused);
        UpdatePauseButton();
    }
    
    void UpdatePauseButton()
    {
        if (PauseState.Paused)
        {
            _pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = ">";
        }
        else
        {
            _pauseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "II";
        }
    }

    void UpdatePassTurnButton()
    {
        _passTurnButton.gameObject.SetActive(PlayerTurnState.InPlayerTurn);
    }

    void OnDisable()
    {
        PauseState.OnPause -= UpdatePauseMenus;
        PauseState.OnUnpause -= UpdatePauseMenus;

        PlayerTurnState.OnPlayerTurnBegin -= UpdatePassTurnButton;
        PlayerTurnState.OnPlayerTurnEnd -= UpdatePassTurnButton;
    }
}
