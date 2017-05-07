using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    [SerializeField]
    private Transform startTransform;

    [SerializeField]
    private Transform endTransform;

    [SerializeField]
    private float movementTimeInSeconds = 5f;

    private IEnumerator Start()
    {
        float t = 0f;

        while(t < 1f)
        {
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, t);
            t += Time.deltaTime / movementTimeInSeconds;
            yield return null;
        }

    }

}
