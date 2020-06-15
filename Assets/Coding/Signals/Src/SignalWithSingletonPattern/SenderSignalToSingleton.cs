using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalToSingleton : MonoBehaviour
{
    IEnumerator Start()
    {
        Debug.Log("SenderSignalToSingleton Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalToSingleton Sending event...");
        ReceiverSignalSingleton.instance.LaunchEvent();
    }
}
