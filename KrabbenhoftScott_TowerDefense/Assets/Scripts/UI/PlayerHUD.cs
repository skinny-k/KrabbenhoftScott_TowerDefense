using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text _resourceText;

    void OnEnable()
    {
        PlayerWallet.OnBalanceUpdate += UpdateResourceText;
    }

    void UpdateResourceText(int balance)
    {
        _resourceText.text = "Resources: " + balance;
    }

    void OnDisable()
    {
        PlayerWallet.OnBalanceUpdate -= UpdateResourceText;
    }
}
