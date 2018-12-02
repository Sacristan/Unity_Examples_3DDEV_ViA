using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalSerializedReference : MonoBehaviour
{
	[SerializeField] private ReceiverSignalSerializedReference receiverSignalSerializedReference;

    IEnumerator Start()
    {
        Debug.Log("SenderSignalSerializedReference Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalSerializedReference Sending event...");
        receiverSignalSerializedReference.LaunchEvent();
    }

}
