using UnityEngine;

namespace Signals.Singleton
{
    public class Receiver : MonoBehaviour
    {
        public static Receiver instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        public void LaunchEvent()
        {
            Debug.Log("Receiver Received EVENT!");
        }
    }
}