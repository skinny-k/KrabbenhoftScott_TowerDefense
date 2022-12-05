using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text _resourceText;
    [SerializeField] HealthBar _playerHealthBar;
    [SerializeField] Image _damageVignette;
    [SerializeField] float _vignetteFadeDelay = 0.5f;
    [SerializeField] float _vignetteFadeSpeed = 0.5f;

    float _vignetteTimer = 0;

    void OnEnable()
    {
        PlayerWallet.OnBalanceUpdate += UpdateResourceText;
        _playerHealthBar.OnTakeDamage += FlashDamageVignette;
    }

    void Update()
    {
        if (_damageVignette.color.a > 0)
        {
            _vignetteTimer += Time.deltaTime;
            if (_vignetteTimer >= _vignetteFadeDelay)
            {
                _damageVignette.color = new Color(1, 1, 1, _damageVignette.color.a - _vignetteFadeSpeed * Time.deltaTime);
            }
        }
    }

    void UpdateResourceText(int balance)
    {
        _resourceText.text = "Resources: " + balance;
    }

    void FlashDamageVignette()
    {
        _damageVignette.color = Color.white;
    }

    void OnDisable()
    {
        PlayerWallet.OnBalanceUpdate -= UpdateResourceText;
        _playerHealthBar.OnTakeDamage -= FlashDamageVignette;
    }
}
