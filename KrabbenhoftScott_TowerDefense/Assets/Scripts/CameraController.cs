using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject _objectToFollow;

    Vector3 _offset;

    void Awake()
    {
        _offset = transform.position - _objectToFollow.transform.position;
    }

    void Update()
    {
        transform.position = _objectToFollow.transform.position + _offset;
    }
}
