using System.Collections;
using UnityEngine;

namespace Signals.Singleton
{
    public class Sender : MonoBehaviour
    {
        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            Receiver.instance.LaunchEvent();
        }
    }
}