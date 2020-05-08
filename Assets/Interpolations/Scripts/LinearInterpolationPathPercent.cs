using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolationPathPercent : MonoBehaviour
{
    [SerializeField]
    private Transform startTransform;

    [SerializeField]
    private Transform endTransform;

    [SerializeField] private float percent = 0.25f;

    private void Start()
    {
        Vector3 a = startTransform.position;
        Vector3 b = endTransform.position;

        Vector3 c = Vector3.Lerp(a, b, percent);

        Debug.LogFormat("Between A {0} B {1} at perc: {2} C is {3}", a, b, percent, c);

        transform.position = c;
    }

}
