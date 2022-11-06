using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected abstract void Fire();
    public abstract void UpgradeTower();
    
    [Header("Move Settings")]
    [SerializeField] protected Transform _turnBody;
    [SerializeField] protected float _turnSpeed = 10f;
    
    [Header("Tower Settings")]
    [SerializeField] protected float _range = 10f;
    [SerializeField] protected float _fireRate = 0.5f;
    [SerializeField] protected int _damage = 5;

    [Header("Construction Settings")]
    [SerializeField] protected int _level = 1;
    [SerializeField] protected int _buildCost = 10;
    [SerializeField] protected int _upgradeCost = 10;

    // public Ability[] abilities = new Ability[5];

    protected TowerPlot _myPlot;
    protected Enemy _target = null;
    protected Vector3 _lookTarget = new Vector3(0, 200000, 0);
    protected float _damageModifier = 1f;
    protected float _turnTimer = 0f;
    protected bool _canFire = true;

    public event Action OnTowerClick;

    public TowerPlot MyPlot
    {
        set => _myPlot = value;
    }
    public float Range
    {
        get => _range;
        set => _range = value;
    }
    public float FireRate
    {
        get => _fireRate;
        set => _fireRate = value;
    }
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }
    public float DamageModifier
    {
        get => _damageModifier;
        set => _damageModifier = value;
    }
    public int Level
    {
        get => _level;
    }
    
    protected virtual void Awake()
    {
        _target = FindTarget();
        UpdateLookTarget();
    }
    
    protected virtual void Update()
    {
        TurnTurret();
    }
    
    protected virtual void FixedUpdate()
    {
        UpdateLookTarget();
        
        if (_canFire)
        {
            Fire();
        }
    }

    void OnMouseDown()
    {
        OnTowerClick?.Invoke();
    }
    
    public virtual void DestroyTower()
    {
        _myPlot.ClearTower();
        Destroy(gameObject);
    }

    protected void UpdateLookTarget()
    {
        if (_target != null)
        {
            _lookTarget = _target.transform.position - transform.position;
            _lookTarget = new Vector3(_lookTarget.x, transform.position.y, _lookTarget.z);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 101) > 99 || _lookTarget.y == 200000)
            {
                float x = UnityEngine.Random.Range(-360.0f, 360.0f);
                float z = UnityEngine.Random.Range(-360.0f, 360.0f);
                _lookTarget = transform.position + new Vector3(x, 0, z);
            }
        }
    }

    protected void TurnTurret()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_lookTarget, Vector3.up);
        float turnModifier = 1f;
        if (_target == null)
        {
            turnModifier = 0.25f;
        }
        _turnBody.rotation = Quaternion.Lerp(_turnBody.rotation, targetRotation, _turnSpeed * turnModifier * Time.deltaTime);
    }
    
    protected Enemy FindTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _range, Vector3.up, _range, LayerMask.GetMask("Enemies"));

        if (hits.Length > 0)
        {
            Player nearestPlayer = FindNearestPlayer();

            if (nearestPlayer != null)
            {
                return GetNearestEnemyTo(nearestPlayer.transform, hits);
            }
            else
            {
                return GetNearestEnemyTo(transform, hits);
            }
        }
        else
        {
            return null;
        }
    }

    protected Enemy GetNearestEnemyTo(Transform objectToDefend, RaycastHit[] enemyHits)
    {
        Transform[] enemiesInRange = new Transform[enemyHits.Length];
            for (int i = 0; i < enemyHits.Length; i++)
            {
                enemiesInRange[i] = enemyHits[i].transform;
            }
            Transform nearestEnemy = enemiesInRange[0];
            foreach (Transform enemy in enemiesInRange)
            {
                if (Vector3.Distance(objectToDefend.position, enemy.position) < Vector3.Distance(objectToDefend.position, nearestEnemy.position))
                {
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy.gameObject.GetComponent<Enemy>();
    }

    protected Player FindNearestPlayer()
    {
        Player[] _allPlayers = (Player[])GameObject.FindObjectsOfType(typeof(Player));
        if (_allPlayers.Length == 0)
        {
            return null;
        }
        else if (_allPlayers.Length == 1)
        {
            return _allPlayers[0];
        }
        else
        {
            Player _currentNearestPlayer = _allPlayers[0];

            for (int i = 1; i < _allPlayers.Length; i++)
            {
                if (Vector3.Distance(transform.position, _allPlayers[i].transform.position) < Vector3.Distance(transform.position, _currentNearestPlayer.transform.position))
                {
                    _currentNearestPlayer = _allPlayers[i];
                }
            }

            return _currentNearestPlayer;
        }
    }

    protected IEnumerator FireCooldown()
    {
        _canFire = false;
        
        yield return new WaitForSeconds(_fireRate);

        _canFire = true;
    }
}
