using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineFakeUpdate : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FakeUpdate());
    }

    IEnumerator FakeUpdate()
    {
        while (true)
        {
            Debug.Log("One Frame Passed");
            yield return null;
        }
    }

    IEnumerator FakeUpdateEndFrame()
    {
        YieldInstruction waitInstruction = new WaitForEndOfFrame();

        while (true)
        {
            Debug.Log("One Frame Passed");
            yield return waitInstruction;
        }
    }

}
