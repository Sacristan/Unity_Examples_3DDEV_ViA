using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        
        Vector3 originPoint = transform.position + Vector3.up * 1.7f; //object position + approximate head height (0,1.7,0) 

        Vector3 rayDirectionVector = transform.forward;

        if (Physics.Raycast(originPoint, rayDirectionVector, out hit))
        {
            Debug.Log("There is an obstacle in front of me!");
            Debug.DrawLine(originPoint, hit.point, Color.red);
        }
        else
        {
            Debug.Log("There is no obstacle ahead of me!");
            Debug.DrawRay(originPoint, rayDirectionVector, Color.green);
        }
    }
}
