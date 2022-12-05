using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlot : MonoBehaviour
{
    [SerializeField] Tower[] _towerPrefabs;

    [Header("Feedback Settings")]
    [SerializeField] ParticleSystem _buildParticles;
    [SerializeField] AudioClip _buildSFX;
    [SerializeField] ParticleSystem _destroyParticles;
    [SerializeField] AudioClip _destroySFX;
    [SerializeField] float _volume = 1f;

    [Header("Indicator Settings")]
    [SerializeField] WorldspaceIcon _buildIndicator;
    [SerializeField] float _towerHeight = 4f;
    
    Tower _currentTower = null;

    public static event Action<TowerPlot> OnPlotClick;

    public static event Action<Tower> OnTowerBuild;
    public static event Action<Tower> OnTowerUpgrade;
    public static event Action OnTowerDestroy;

    public Tower CurrentTower
    {
        get => _currentTower;
        set => _currentTower = value;
    }

    void OnEnable()
    {
        PlayerTurnState.OnPlayerTurnBegin += DisplayBuildIndicator;
        PlayerTurnState.OnPlayerTurnEnd += HideBuildIndicator;
    }

    public void BuildTower<T>() where T : Tower
    {
        Tower towerToSpawn = null;
        
        foreach (Tower towerType in _towerPrefabs)
        {
            if (towerType is T)
            {
                towerToSpawn = towerType;
                break;
            }
        }

        if (Player.PlayerResources.Balance >= towerToSpawn.BuildCost)
        {
            _currentTower = Instantiate(towerToSpawn, transform);
            Player.WithdrawFunds(_currentTower.BuildCost);
            _currentTower.MyPlot = this;
            _currentTower.OnTowerClick += TowerClick;

            if (_buildIndicator != null)
            {
                _buildIndicator.AdjustBasePosition(new Vector3(0, _towerHeight, 0));
            }

            Feedback(_buildSFX, false, _buildParticles);

            OnTowerBuild?.Invoke(_currentTower);
        }
    }

    public void UpgradeTower()
    {
        if (Player.PlayerResources.Balance >= _currentTower.UpgradeCost)
        {
            Player.WithdrawFunds(_currentTower.UpgradeCost);
            _currentTower.UpgradeTower();

            Feedback(_buildSFX, false, _buildParticles);

            OnTowerUpgrade?.Invoke(_currentTower);
        }
    }

    public void DestroyTower()
    {
        Player.DepositFunds(_currentTower.BuildCost);
        _currentTower.DestroyTower();
        if (_buildIndicator != null)
        {
            _buildIndicator.AdjustBasePosition(new Vector3(0, -_towerHeight, 0));
        }
        OnTowerDestroy?.Invoke();
    }

    public void ClearTower()
    {
        _currentTower.OnTowerClick -= TowerClick;
        _currentTower = null;
    }

    void OnMouseDown()
    {
        OnPlotClick?.Invoke(this);
    }

    void TowerClick()
    {
        OnPlotClick?.Invoke(this);
    }

    void DisplayBuildIndicator()
    {
        _buildIndicator.gameObject.SetActive(true);
    }

    void HideBuildIndicator()
    {
        _buildIndicator.gameObject.SetActive(false);
    }

    void Feedback(AudioClip sfx = null, bool playAs3D = false, ParticleSystem particles = null)
    {
        if (sfx != null)
        {
            if (playAs3D)
            {
                AudioHelper.PlayClip3D(sfx, _volume, transform.position);
            }
            else
            {
                AudioHelper.PlayClip2D(sfx, _volume);
            }
        }

        if (particles != null)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }

    void OnDisable()
    {
        PlayerTurnState.OnPlayerTurnBegin -= DisplayBuildIndicator;
        PlayerTurnState.OnPlayerTurnEnd -= HideBuildIndicator;
    }
}
