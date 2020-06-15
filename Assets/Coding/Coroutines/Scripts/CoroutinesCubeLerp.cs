using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutinesCubeLerp : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float colorInterpolationTime = 3f;

    private void Start()
    {
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
        cube.transform.localScale *= 2f;
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


}
