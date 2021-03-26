using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals.UnityEvents
{
    public class Sender : MonoBehaviour
    {
        [SerializeField] UnityEvent signalEvent;

        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            signalEvent?.Invoke();
        }
    }
}