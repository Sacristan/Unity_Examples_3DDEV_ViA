using System.Collections;
using UnityEngine;

public class CoroutineStruct : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(SomeRoutine());
    }

    private IEnumerator SomeRoutine()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(3f); //Wait for 3 seconds
        Debug.Log("Three seconds passed!");

        Debug.Log("Lets wait for one frame");
        yield return null; //Wait until the next frame

        Debug.Log("Routine finished and will exit!");
    }

}
