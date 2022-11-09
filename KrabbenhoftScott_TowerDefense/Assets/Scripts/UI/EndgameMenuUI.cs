using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameMenuUI : MonoBehaviour
{
    [SerializeField] GameObject _winMenu;
    [SerializeField] GameObject _loseMenu;
    
    void OnEnable()
    {
        WinState.OnWinStateEnter += ActivateWinMenu;
        LoseState.OnLoseStateEnter += ActivateLoseMenu;

        WinState.OnWinStateExit += DeactivateMenus;
        LoseState.OnLoseStateExit += DeactivateMenus;
    }

    void ActivateWinMenu()
    {
        _winMenu.SetActive(true);
    }

    void ActivateLoseMenu()
    {
        _loseMenu.SetActive(true);
    }

    void DeactivateMenus()
    {
        _winMenu.SetActive(false);
        _loseMenu.SetActive(false);
    }

    void OnDisable()
    {
        WinState.OnWinStateEnter -= ActivateWinMenu;
        LoseState.OnLoseStateEnter -= ActivateLoseMenu;

        WinState.OnWinStateExit -= DeactivateMenus;
        LoseState.OnLoseStateExit -= DeactivateMenus;
    }
}
