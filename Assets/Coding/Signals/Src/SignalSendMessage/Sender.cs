using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Signals.SendMessage
{
    public class Sender : MonoBehaviour
    {
        IEnumerator Start()
        {
            Debug.Log("Sender Waiting for 1 second...");
            yield return new WaitForSeconds(1f);
            Debug.Log("Sender Sending event...");
            /*
                BAD PRACTICE
                    A: GameObject found using it's name
                    B: Method is referenced by it's name string val - changing the name will break this wout compilation errors
            */
            GameObject.Find("Receiver").SendMessage("LaunchEvent");
        }

    }
}