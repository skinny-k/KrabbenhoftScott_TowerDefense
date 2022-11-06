using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuUI : MonoBehaviour
{
    [SerializeField] GameObject GameController;
    [SerializeField] RectTransform BuildMenu;
    [SerializeField] RectTransform UpgradeMenu;

    static TowerMenuState _menuState;
    static bool _active = false;
    
    public static TowerMenuUI Instance;
    
    public RectTransform TowerMenu; 
    public TowerPlot CurrentPlot = null;
    
    void OnEnable()
    {
        TowerPlot.OnPlotClick += SetCurrentTowerPlot;
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _menuState = Instance.GameController.GetComponent<TowerMenuState>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _active = true;
        }
    }

    public static void UpdateMenuPlacement()
    {
        Vector2 canvasSize = Instance.TowerMenu.GetComponentInParent<Canvas>().renderingDisplaySize;
        Vector3 offset = new Vector3(canvasSize.x * 0.5f, canvasSize.y * 0.5f, 0);

        Instance.TowerMenu.anchoredPosition3D = Camera.main.WorldToScreenPoint(Instance.CurrentPlot.transform.position) - offset;
    }

    public static void UpdateMenuContent()
    {
        if (Instance.CurrentPlot.CurrentTower == null)
        {
            Instance.TowerMenu = Instance.BuildMenu;
        }
        else
        {
            Instance.TowerMenu = Instance.UpgradeMenu;

            if (Instance.CurrentPlot.CurrentTower.Level >= 4)
            {
                Instance.UpgradeMenu.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            else
            {
                Instance.UpgradeMenu.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    static void SetCurrentTowerPlot(TowerPlot plot)
    {
        Instance.CurrentPlot = plot;
    }
    
    public static void OpenMenu()
    {
        _active = false;
        Instance.TowerMenu.gameObject.SetActive(true);
    }

    public static void CloseMenu()
    {
        Instance.CurrentPlot = null;
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

    public static void UpgradeTower()
    {
        if (_active && Instance.CurrentPlot.CurrentTower != null)
        {
            Instance.CurrentPlot.UpgradeTower();
        }
    }

    public static void DestroyTower()
    {
        if (_active && Instance.CurrentPlot.CurrentTower != null)
        {
            Instance.CurrentPlot.DestroyTower();
        }
    }

    void OnDisable()
    {
        TowerPlot.OnPlotClick += SetCurrentTowerPlot;
    }
}
