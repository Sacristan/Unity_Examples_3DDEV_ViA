using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalDelegateGlobalEvent : MonoBehaviour
{
    public delegate void SignalEventHandler();
    public static event SignalEventHandler OnSignalSent;

    IEnumerator Start()
    {
        Debug.Log("SenderSignalDelegateGlobalEvent Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalDelegateGlobalEvent Sending event...");
        if(OnSignalSent != null) OnSignalSent.Invoke();
    }
}
