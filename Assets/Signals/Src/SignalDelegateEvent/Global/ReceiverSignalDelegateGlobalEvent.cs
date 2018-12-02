using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverSignalDelegateGlobalEvent : MonoBehaviour
{
    private void Start()
    {
        SenderSignalDelegateGlobalEvent.OnSignalSent += LaunchEvent;
    }

    private void OnDestroy()
    {
        SenderSignalDelegateGlobalEvent.OnSignalSent -= LaunchEvent;
    }

    private void LaunchEvent()
    {
    	Debug.Log("ReceiverSignalDelegateGlobalEvent Received EVENT!");
    }
}
