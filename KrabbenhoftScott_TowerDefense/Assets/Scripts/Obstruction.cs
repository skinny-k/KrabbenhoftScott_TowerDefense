using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    [SerializeField] float _delayBeforeDespawn = 0.5f;
    
    protected float _duration;
    protected float _timer = 0f;
    
    public virtual void Initialize(float duration)
    {
        _duration = duration;
        SubscribeToEvents();
    }

    protected virtual void SubscribeToEvents()
    {
        PlayerTurnState.OnPlayerTurnBegin += DestroySelf;
    }

    protected virtual void UnsubscribeToEvents()
    {
        PlayerTurnState.OnPlayerTurnBegin -= DestroySelf;
    }

    protected virtual void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= _duration)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void DestroySelf()
    {
        StartCoroutine(Despawn());
    }

    protected virtual IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_delayBeforeDespawn);

        Destroy(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
