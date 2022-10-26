using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenuUI : MonoBehaviour
{
    public static TowerMenuUI Instance;
    
    public RectTransform TowerMenu; 
    public TowerPlot _currentPlot = null;

    public TowerPlot CurrentPlot
    {
        get => _currentPlot;
        set => _currentPlot = value;
    }
    
    void Awake()
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

    public static void UpdateMenuPlacement()
    {
        Vector2 canvasSize = Instance.TowerMenu.GetComponentInParent<Canvas>().renderingDisplaySize;
        Vector3 offset = new Vector3(canvasSize.x * 0.5f, canvasSize.y * 0.5f, 0);

        Instance.TowerMenu.anchoredPosition3D = Camera.main.WorldToScreenPoint(Instance.CurrentPlot.transform.position) - offset;
    }
    
    public static void OpenMenu()
    {
        Instance.TowerMenu.gameObject.SetActive(true);
    }

    public static void CloseMenu()
    {
        Instance.TowerMenu.gameObject.SetActive(false);
    }

    public static void BuildPhysicalTower()
    {
        if (Instance.CurrentPlot.CurrentTower == null)
        {
            Instance.CurrentPlot.BuildTower<PhysicalTower>();
        }
    }

    public static void BuildSpecialTower()
    {
        if (Instance.CurrentPlot.CurrentTower == null)
        {
            Instance.CurrentPlot.BuildTower<SpecialTower>();
        }
    }

    public static void BuildSplashTower()
    {
        if (Instance.CurrentPlot.CurrentTower == null)
        {
            Instance.CurrentPlot.BuildTower<SplashTower>();
        }
    }
}
