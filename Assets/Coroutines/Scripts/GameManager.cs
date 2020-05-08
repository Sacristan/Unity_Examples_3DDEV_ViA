using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States
    {
        Unknown,
        Idle,
        Walking,
        Attacking
    }

    [SerializeField] private States currentState;
    private States lastKnownState = States.Unknown;

    private void Start()
    {
        StartCoroutine(StateMachine());
    }

    /// <summary>
    /// When current state is changed - state machine will react
    /// </summary>
    /// <returns></returns>
    private IEnumerator StateMachine()
    {
        while (true) //This will be working until object will be disabled or destroyed
        {
            if (lastKnownState == States.Unknown || currentState != lastKnownState) //if last known state is Unknown or current state is not the same as last known state
            {
                switch (currentState)
                {
                    case States.Idle:
                        StartIdle();
                        break;
                    case States.Walking:
                        StartWalk();
                        break;
                    case States.Attacking:
                        StartAttack();
                        break;
                    default:
                        Debug.LogWarningFormat("StateMachine caught unsupported state: {0}", currentState.ToString());
                        break;
                }

                lastKnownState = currentState; //Last known state is now the current state
            }

            yield return null; // Wait for one frame
        }
    }

    private void StartIdle()
    {
        Debug.Log("Starting " + currentState + " state");
    }

    private void StartWalk()
    {
        Debug.Log("Starting " + currentState + " state");
    }

    private void StartAttack()
    {
        Debug.Log("Starting " + currentState + " state");
    }
}
