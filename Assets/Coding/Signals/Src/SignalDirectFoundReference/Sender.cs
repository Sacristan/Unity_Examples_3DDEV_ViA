using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals.FoundReference
{
    public class Sender : MonoBehaviour
    {
        IEnumerator Start()
        {
            Receiver receiver = FindObjectOfType<Receiver>();

            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            receiver.LaunchEvent();
        }

    }
}