using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2f;

    Rigidbody _rb;
    Transform _hands;
    float _moveModifier = 1f;
    bool _hasControl = false;

    void OnEnable()
    {
        PlayerTurnState.OnPlayerTurnBegin += GainControl;

        PauseState.OnPause += ReleaseControl;
        PauseState.OnUnpause += GainControl;

        WinState.OnWinStateEnter += ReleaseControl;
        WinState.OnWinStateExit += GainControl;

        LoseState.OnLoseStateEnter += ReleaseControl;
        LoseState.OnLoseStateExit += GainControl;
    }
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _hands = transform.GetChild(0);
    }
    
    void FixedUpdate()
    {
        if (_hasControl)
        {
            MovePlayer();
            TurnPlayer();
        }
    }

    void MovePlayer()
    {
        // calculate the move amount
        Vector3 moveOffset = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset * _moveSpeed * _moveModifier * Time.deltaTime);
        // technically adjusting vector is more accurate! (but more complex)

        if (moveOffset != Vector3.zero)
        {
            _hands.localRotation = Quaternion.Euler(0, 40f * Mathf.Sin(Time.time * 7.5f), 0);
        }
        else
        {
            _hands.localRotation = Quaternion.Lerp(_hands.localRotation, Quaternion.Euler(Vector3.zero), 10f);
        }
    }

    void TurnPlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100);
        if (hit.point != null)
        {
            //_lookTarget.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public IEnumerator ModifySpeed(float moveModifier, float duration)
    {
        _moveModifier *= moveModifier;

        yield return new WaitForSeconds(duration);

        _moveModifier /= moveModifier;
    }

    void ReleaseControl()
    {
        _hasControl = false;
    }

    void GainControl()
    {
        _hasControl = true;
    }

    void OnDisable()
    {
        PlayerTurnState.OnPlayerTurnBegin -= GainControl;

        PauseState.OnPause -= ReleaseControl;
        PauseState.OnUnpause -= GainControl;

        WinState.OnWinStateEnter -= ReleaseControl;
        WinState.OnWinStateExit -= GainControl;

        LoseState.OnLoseStateEnter -= ReleaseControl;
        LoseState.OnLoseStateExit -= GainControl;
    }
}
