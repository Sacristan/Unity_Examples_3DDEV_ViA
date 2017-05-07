using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;
        
        Vector3 source = transform.position;
        Vector3 target = transform.forward;

        Vector3 debugTarget;

        if (Physics.Raycast(source, target, out hit))
        {
            debugTarget = hit.point;
            print("There is an obstacle in front of me!");
        }
        else
        {
            debugTarget = target;
            print("There is no obstacle ahead of me!");
        }

        Debug.DrawLine(source, debugTarget, Color.blue);
            
    }
}
