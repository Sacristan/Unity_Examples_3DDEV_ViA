using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    private enum State
    {
        Idle,
        Jumping,
        Dead
    }

    [SerializeField]
    private State currentState = State.Idle;
    private State lastState;

    private bool stateMachineInitiated = false;

    private Renderer _renderer;
    private Vector3 _originalPosition;
    private float startedJumpingTime;

    private void Start()
    {
        _originalPosition = transform.position;
        _renderer = GetComponent<Renderer>();

        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            if (!stateMachineInitiated || lastState != currentState)
            {

                switch (currentState)
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

                stateMachineInitiated = true;
            }

            StateMachineUpdate();

            lastState = currentState;
            yield return null;
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

    private void StateMachineUpdate()
    {
        switch (currentState)
        {
            case State.Jumping:
                Vector3 newPosition = transform.position;
                newPosition.y = _originalPosition.y + Mathf.PingPong(Time.time - startedJumpingTime, 1.5f);
                transform.position = newPosition;
                break;
        }
    }
}
