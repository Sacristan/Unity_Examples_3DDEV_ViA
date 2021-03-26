using UnityEngine;

namespace Signals.DelegateEvents.Global
{
    public class Receiver : MonoBehaviour
    {
        private void Start()
        {
            Sender.OnSignalSent += LaunchEvent;
        }

        private void OnDestroy()
        {
            Sender.OnSignalSent -= LaunchEvent;
        }

        private void LaunchEvent()
        {
            Debug.Log("Receiver Received EVENT!");
        }
    }
}