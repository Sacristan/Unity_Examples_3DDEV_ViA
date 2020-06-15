using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalFoundReference : MonoBehaviour
{
    IEnumerator Start()
    {
        ReceiverSignalFoundReference receiverSignalFoundReference = FindObjectOfType<ReceiverSignalFoundReference>();

        Debug.Log("SenderSignalFoundReference Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalFoundReference Sending event...");
        receiverSignalFoundReference.LaunchEvent();
    }

}
