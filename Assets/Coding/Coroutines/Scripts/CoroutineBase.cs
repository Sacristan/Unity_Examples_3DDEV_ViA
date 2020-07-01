using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineBase : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Waited for one second!");
        yield return new WaitForSeconds(1f);
        Debug.Log("Waited for another second!");
    }

}
