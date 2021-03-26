using UnityEngine;

namespace Signals.UnityEvents
{
    public class Receiver : MonoBehaviour
    {
        public void LaunchEvent()
        {
            Debug.Log("Receiver Received EVENT!");
        }
    }
}