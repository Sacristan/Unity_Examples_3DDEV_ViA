using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{   
    float fps;

    void Update()
    {
        fps = 1f / Time.deltaTime;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 50, 0, 100, 50), fps.ToString("n2"));    
    }
}
