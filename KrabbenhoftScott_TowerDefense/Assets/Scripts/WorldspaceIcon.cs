using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceIcon : MonoBehaviour
{
    [SerializeField] float _bobAmplitude = 1f;
    [SerializeField] float _bobPeriod = 2f;

    Vector3 _startPosition;

    void Awake()
    {
        _startPosition = transform.position;
    }
    
    void Update()
    {
        transform.LookAt(Camera.main.transform);

        transform.position = _startPosition + new Vector3(0, _bobAmplitude * Mathf.Sin(((2 * Mathf.PI) / _bobPeriod) * Time.time), 0);
    }

    public void AdjustBasePosition(Vector3 offset)
    {
        _startPosition += offset;
    }
}
