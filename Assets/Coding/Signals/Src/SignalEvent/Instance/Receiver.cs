using UnityEngine;

namespace Signals.DelegateEvents.Instance
{
    public class Receiver : MonoBehaviour
    {
        private void Start()
        {
            Sender sender = FindObjectOfType<Sender>();
            sender.OnSignalSent += LaunchEvent;
        }

        private void LaunchEvent()
        {
            Debug.Log("Receiver Received EVENT!");
        }
    }
}