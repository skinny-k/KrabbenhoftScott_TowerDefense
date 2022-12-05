using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
    [SerializeField] Color _playerTurnColor = Color.yellow;
    [SerializeField] Color _enemyTurnColor = Color.red;
    
    void OnEnable()
    {
        PlayerTurnState.OnPlayerTurnBegin += EnterPlayerTurn;
        EnemyTurnState.OnEnemyTurnBegin += EnterEnemyTurn;
    }
    
    void EnterPlayerTurn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = _playerTurnColor;
            transform.GetChild(i).localScale = new Vector3(1, 1, 1);
        }
    }

    void EnterEnemyTurn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = _enemyTurnColor;
            transform.GetChild(i).localScale = new Vector3(-1, 1, 1);
        }
    }

    void OnDisable()
    {
        PlayerTurnState.OnPlayerTurnBegin -= EnterPlayerTurn;
        EnemyTurnState.OnEnemyTurnBegin -= EnterEnemyTurn;
    }
}
