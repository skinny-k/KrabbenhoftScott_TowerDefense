using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlot : MonoBehaviour
{
    [SerializeField] Tower[] _towerPrefabs;
    
    Tower _currentTower = null;

    public static event Action<TowerPlot> OnPlotClick;
    public static event Action<Tower> OnTowerBuild;

    public Tower CurrentTower
    {
        get => _currentTower;
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

        _currentTower = Instantiate(towerToSpawn, transform);
        _currentTower.MyPlot = this;
        _currentTower.OnTowerClick += TowerClick;

        OnTowerBuild?.Invoke(_currentTower);
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
}
