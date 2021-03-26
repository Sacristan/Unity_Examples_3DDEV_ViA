using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals.SerializedReference
{
    public class Sender : MonoBehaviour
    {
        [SerializeField] private Receiver receiver;

        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            receiver.LaunchEvent();
        }

    }
}