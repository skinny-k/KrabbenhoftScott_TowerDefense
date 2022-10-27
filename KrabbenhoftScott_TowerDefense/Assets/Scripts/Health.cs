using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [Header("Health Settings")]
    [SerializeField] protected int _maxHealth = 30;
    [SerializeField] protected float _healModifier = 1f;
    [SerializeField] protected float _iFrames = 0.5f;

    [Header("Defense Settings")]
    [SerializeField] protected float _physicalDefenseModifier = 0f;
    [SerializeField] protected float _specialDefenseModifier = 0f;

    [Header("Feedback Settings")]
    [SerializeField] protected ParticleSystem _damageParticles;
    [SerializeField] protected AudioClip _damageSFX;
    [SerializeField] protected ParticleSystem _dieParticles;
    [SerializeField] protected AudioClip _dieSFX;
    [SerializeField] protected Color _damageEmissionColor = Color.black;
    [SerializeField] protected float _SFXVolume = 1f;

    public event Action OnSpawn;
    public event Action OnTakeDamage;
    public event Action OnDie;
    public event Action OnHeal;

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public int CurrentHealth
    {
        get => _currentHealth;
    }
    public float PhysicalDefenseModifier
    {
        set => _physicalDefenseModifier = value;
    }
    public float SpecialDefenseModifier
    {
        set => _specialDefenseModifier = value;
    }

    MeshRenderer _meshRenderer;
    Color _initialEmissionColor;
    protected float _lastDamage;
    protected int _currentHealth;

    public virtual void Awake()
    {
        _currentHealth = _maxHealth;
        _lastDamage = _iFrames;
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer != null)
        {
            _initialEmissionColor = _meshRenderer.material.GetColor("_EmissionColor");
        }
        OnSpawn?.Invoke();
    }

    void FixedUpdate()
    {
        if (_lastDamage < _iFrames)
        {
            _lastDamage += Time.deltaTime;
        }
    }
    
    public virtual void DecreaseHealth(int damage, bool isSpecial)
    {
        if (_lastDamage >= _iFrames)
        {
            float defenseModifier;
            if (isSpecial)
            {
                defenseModifier = _specialDefenseModifier;
            }
            else
            {
                defenseModifier = _physicalDefenseModifier;
            }
            
            _currentHealth = (int)Mathf.Clamp(_currentHealth - (damage * (1f - Mathf.Clamp(defenseModifier, -1f, 1f))), 0, _maxHealth);
            if (_currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                if (_damageParticles != null)
                {
                    Instantiate(_damageParticles, transform);
                }
                else if (_meshRenderer != null)
                {
                    StartCoroutine(FlashRed(_iFrames - 0.1f));
                }

                if (_damageSFX != null)
                {
                    AudioHelper.PlayClip3D(_damageSFX, _SFXVolume, transform.position);
                }
           }
        
            OnTakeDamage?.Invoke();
            _lastDamage = 0;
        }
    }

    public void IncreaseHealth(int heal)
    {
        _currentHealth = (int)Mathf.Clamp(_currentHealth + (heal * _healModifier), 0, _maxHealth);

        OnHeal?.Invoke();
    }

    protected virtual void Kill()
    {
        if (_dieParticles != null)
        {
            Instantiate(_dieParticles, transform.position, transform.rotation);
        }
        if (_dieSFX != null)
        {
            AudioHelper.PlayClip3D(_dieSFX, _SFXVolume, transform.position);
        }
        
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }

    IEnumerator FlashRed(float duration)
    {
        _meshRenderer.material.SetColor("_EmissionColor", _damageEmissionColor);

        yield return new WaitForSeconds(duration);

        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", _initialEmissionColor);
    }
}
