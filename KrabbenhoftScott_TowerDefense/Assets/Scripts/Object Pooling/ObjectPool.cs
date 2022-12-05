using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    [Header("Pool Settings")]
    [SerializeField] T _prefab = null;
    [SerializeField] int _startingPoolSize = 10;

    public T Prefab
    {
        get => _prefab;
    }

    protected Queue<T> _objectPool = new Queue<T>();
    
    protected virtual void Start()
    {
        CreateInitialPool();
    }

    protected virtual void CreateInitialPool()
    {
        for (int i = 0; i < _startingPoolSize; i++)
        {
            CreateNewPoolObject();
        }
    }

    protected virtual void CreateNewPoolObject()
    {
        T newObj = Instantiate(_prefab);
        // ResetObjectToDefaults(newObj);
        newObj.transform.SetParent(transform);
        newObj.gameObject.name = _prefab.gameObject.name;
        newObj.gameObject.SetActive(false);

        _objectPool.Enqueue(newObj);
    }
    
    public virtual T ActivateFromPool()
    {
        if (_objectPool.Count == 0)
        {
            CreateNewPoolObject();
        }
        
        T objToActivate = _objectPool.Dequeue();
        objToActivate.gameObject.SetActive(true);
        ResetObjectToDefaults(objToActivate);
        
        // Debug.Log("Returning " + objToActivate.gameObject.name);
        return objToActivate;
    }

    public virtual void ReturnToPool(T objToReturn)
    {
        // ResetObjectToDefaults(objToReturn);
        objToReturn.gameObject.SetActive(false);
        _objectPool.Enqueue(objToReturn);
    }

    protected virtual void ResetObjectToDefaults(T objToReset)
    {
        //
    }
}
