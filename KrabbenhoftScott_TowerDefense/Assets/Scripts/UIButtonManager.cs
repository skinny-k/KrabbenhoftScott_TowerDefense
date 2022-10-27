using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] Button _pauseButton;
    [SerializeField] Button _passTurnButton;

    void OnEnable()
    {
        PauseState.OnPause += UpdatePauseButton;
        PauseState.OnUnpause += UpdatePauseButton;

        PlayerTurnState.OnPlayerTurnBegin += UpdatePassTurnButton;
        PlayerTurnState.OnPlayerTurnEnd += UpdatePassTurnButton;
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
        PauseState.OnPause -= UpdatePauseButton;
        PauseState.OnUnpause -= UpdatePauseButton;

        PlayerTurnState.OnPlayerTurnBegin -= UpdatePassTurnButton;
        PlayerTurnState.OnPlayerTurnEnd -= UpdatePassTurnButton;
    }
}
