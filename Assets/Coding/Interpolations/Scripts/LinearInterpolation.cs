using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    [SerializeField] private Transform startTransform;

    [SerializeField] private Transform endTransform;

    [SerializeField] private float movementTimeInSeconds = 5f;

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime / movementTimeInSeconds;
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, t);
            yield return null;
        }
    }

}
