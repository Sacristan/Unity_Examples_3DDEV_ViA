using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenderSignalSendMessage : MonoBehaviour
{

    IEnumerator Start()
    {
        Debug.Log("SenderSignalSendMessage Waiting for 1 second...");
        yield return new WaitForSeconds(1f);
        Debug.Log("SenderSignalSendMessage Sending event...");

        GameObject.Find("Receiver").SendMessage("LaunchEvent");
    }

}
