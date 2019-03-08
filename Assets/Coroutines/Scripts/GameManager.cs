using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States
    {
        Unknown,
        EatingPizza,
        WatchingAtBirds,
        WalkingAwkwardly
    }
    
    [SerializeField]
    private GameObject cube;

    [SerializeField]
    private States currentState;

    [SerializeField] private float colorInterpolationTime = 3f;

    private States lastKnownState = States.Unknown;

    private void Start()
    {
        StartCoroutine(StateMachine());
        StartCoroutine(CubeBehaviour());
    }

    private IEnumerator CubeBehaviour()
    {
        Debug.Log("Scaling cube up...");
        yield return DoubleTheSize(); //Waiting until Enumerator DoubleTheSize returns

        yield return new WaitForSeconds(2f); // wait for one second


        Debug.Log("Changing cube color");
        yield return ChangeToColor(Color.blue); //Waiting until Change color done
        Debug.Log("CubeBehaviour done!");
    }

    private IEnumerator DoubleTheSize()
    {
        yield return new WaitForSeconds(1f); // wait for one second
        cube.transform.localScale = cube.transform.localScale * 2f;
    }

    private IEnumerator ChangeToColor(Color targetColor)
    {
        float t = 0f; //time variable that will hold the time between 0..1

        Renderer renderer = cube.GetComponent<Renderer>();
        Color originalColor = renderer.material.color;

        while (t < 1f) //while time is not over or equal to 1f (100%)
        {
            t += Time.deltaTime / colorInterpolationTime; // increase percentage by 1/fps

            Color newColor = Color.Lerp(originalColor, targetColor, t); //Interpolate linearly between original color and target color using T as percentage
            renderer.material.color = newColor; // set new color
            yield return null;// wait for the next frame
        }
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
                    case States.EatingPizza:
                        EatingPizza();
                        break;
                    case States.WalkingAwkwardly:
                        WalkingAwkwardly();
                        break;
                    case States.WatchingAtBirds:
                        WatchingAtBirds();
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

    private void EatingPizza()
    {
        Debug.Log("Im eating tasty pizza!");
    }
    private void WalkingAwkwardly()
    {
        Debug.Log("Really? Am i walking awkwardly?");
    }

    private void WatchingAtBirds()
    {
        Debug.Log("Watching birds is the new cool!");
    }
}
