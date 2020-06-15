using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverSignalSingleton : MonoBehaviour
{
    public static ReceiverSignalSingleton instance;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void LaunchEvent()
    {
    	Debug.Log("ReceiverSignalSingleton Received EVENT!");
    }
}
