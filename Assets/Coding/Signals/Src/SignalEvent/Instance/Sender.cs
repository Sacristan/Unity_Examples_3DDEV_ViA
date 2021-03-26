using System.Collections;
using UnityEngine;

namespace Signals.DelegateEvents.Instance
{
    public class Sender : MonoBehaviour
    {
        public delegate void SignalEventHandler();
        public event System.Action OnSignalSent;

        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            OnSignalSent?.Invoke();
        }
    }
}