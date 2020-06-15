using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverSignalDelegateInstanceEvent : MonoBehaviour
{
    private void Start()
    {
        SenderSignalDelegateInstanceEvent senderSignalDelegateInstanceEvent = FindObjectOfType<SenderSignalDelegateInstanceEvent>();
        senderSignalDelegateInstanceEvent.OnSignalSent += LaunchEvent;
    }

    private void LaunchEvent()
    {
    	Debug.Log("ReceiverSignalDelegateInstanceEvent Received EVENT!");
    }
}
