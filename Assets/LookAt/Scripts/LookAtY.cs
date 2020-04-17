using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtY : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] LineRenderer laser;
    [SerializeField] float maxLaserDistance = 1f;

    private void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.transform.position;

        Vector3 lookAtDir = (b - a).normalized;
        float dist = Vector3.Distance(a, b);

        DebugRay(a, lookAtDir, dist);
        UpdateLaser(a, lookAtDir, dist);
        LookToDirectionIgnoreY(lookAtDir);
    }

    private void DebugRay(Vector3 startPos, Vector3 dir, float dist)
    {
        /*
            From: start pos
            To: start pos across direction with distance from target
        */
        Debug.DrawLine(startPos, startPos + dir * dist, Color.green);
    }

    private void UpdateLaser(Vector3 startPos, Vector3 dir, float dist)
    {
        Vector3 endPos = startPos + dir * Mathf.Clamp(dist, 0f, maxLaserDistance);

        laser.SetPosition(0, startPos); //StartPos
        laser.SetPosition(1, endPos); //EndPos

    }

    private void LookToDirectionIgnoreY(Vector3 dir)
    {
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

}
