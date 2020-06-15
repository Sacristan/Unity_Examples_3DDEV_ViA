using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalDelegateInstanceEvent : MonoBehaviour
{
    public delegate void SignalEventHandler();
    public event SignalEventHandler OnSignalSent;

    IEnumerator Start()
    {
        Debug.Log("SenderSignalDelegateInstanceEvent Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalDelegateInstanceEvent Sending event...");
        if(OnSignalSent != null) OnSignalSent.Invoke();
    }
}
