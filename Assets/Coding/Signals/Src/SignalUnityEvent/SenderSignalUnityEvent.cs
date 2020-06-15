using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SenderSignalUnityEvent : MonoBehaviour
{
    [SerializeField] UnityEvent signalEvent;

    IEnumerator Start()
    {
        Debug.Log("SenderSignalUnityEvent Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalUnityEvent Sending event...");
        if(signalEvent != null) signalEvent.Invoke();
    }
}
