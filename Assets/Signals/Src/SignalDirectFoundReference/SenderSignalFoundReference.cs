using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalFoundReference : MonoBehaviour
{
	private ReceiverSignalFoundReference _receiverSignalFoundReference;

    private void Awake()
    {
        _receiverSignalFoundReference = FindObjectOfType<ReceiverSignalFoundReference>();
    }

    IEnumerator Start()
    {
        Debug.Log("SenderSignalFoundReference Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalFoundReference Sending event...");
        _receiverSignalFoundReference.LaunchEvent();
    }

}
