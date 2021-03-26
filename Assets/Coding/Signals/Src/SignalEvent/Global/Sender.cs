using System.Collections;
using UnityEngine;

namespace Signals.DelegateEvents.Global
{
    public class Sender : MonoBehaviour
    {
        public static event System.Action OnSignalSent;

        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            OnSignalSent?.Invoke();
        }
    }
}