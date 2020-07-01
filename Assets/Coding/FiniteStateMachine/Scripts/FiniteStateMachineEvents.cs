using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachineEvents : MonoBehaviour
{
    private enum State
    {
        None,
        Idle,
        Jumping,
        Dead
    }

    private State currentState = State.None;
    private State lastState;

    private bool stateMachineInitiated = false;

    private Renderer _renderer;
    private Vector3 _originalPosition;
    private float startedJumpingTime;

    private State CurrentState
    {
        get => currentState;
        set
        {
            if (currentState != value)
            {
                currentState = value;
                UpdateState();
            }
        }
    }

    private IEnumerator Start()
    {
        _originalPosition = transform.position;
        _renderer = GetComponent<Renderer>();

        CurrentState = State.Idle;

        yield return new WaitForSeconds(2f);
        CurrentState = State.Jumping;

        yield return new WaitForSeconds(5f);
        CurrentState = State.Dead;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case State.Jumping:
                Vector3 newPosition = transform.position;
                newPosition.y = _originalPosition.y + Mathf.PingPong(Time.time - startedJumpingTime, 1.5f);
                transform.position = newPosition;
                break;
        }
    }

    void UpdateState()
    {
        switch (CurrentState)
        {
            case State.Idle:
                InitIdle();
                break;
            case State.Jumping:
                InitJumping();
                break;
            case State.Dead:
                InitDead();
                break;
        }
    }

    private void InitIdle()
    {
        Debug.LogFormat("Initiated {0} State", currentState.ToString());

        _renderer.material.color = Color.blue;
        transform.position = _originalPosition;
    }

    private void InitJumping()
    {
        Debug.LogFormat("Initiated {0} State", currentState.ToString());

        _renderer.material.color = Color.yellow;

        startedJumpingTime = Time.time;
    }

    private void InitDead()
    {
        Debug.LogFormat("Initiated {0} State", currentState.ToString());

        _renderer.material.color = Color.black;
        transform.position = _originalPosition;
    }


}
