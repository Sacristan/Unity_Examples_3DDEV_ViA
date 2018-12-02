using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverSignalSingleton : MonoBehaviour
{
    public static ReceiverSignalSingleton instance;

    private void Awake()
    {
        instance = this;
    }

    public void LaunchEvent()
    {
    	Debug.Log("ReceiverSignalSingleton Received EVENT!");
    }
}
