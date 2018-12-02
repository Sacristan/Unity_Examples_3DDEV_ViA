using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseLinearInterpolation : MonoBehaviour
{
    [SerializeField] Transform a;
    [SerializeField] Transform b;


    private void Start()
    {
        float ax = a.position.x;
        float bx = b.position.x;
        float cx = transform.position.x;

        float t = Mathf.InverseLerp(ax, bx, cx);
        Debug.LogFormat("Cx:{0} progress percentage between Ax: {1} Bx {2} IS -> {3}", cx, ax, bx, t);
    }

}
