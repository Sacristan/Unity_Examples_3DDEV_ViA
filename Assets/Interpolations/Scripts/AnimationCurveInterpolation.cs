using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveInterpolation : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve = AnimationCurve.Linear(0f,0f,1f,1f); //Default value is Linear Intepolation curve

    [SerializeField]
    private Transform startTransform;

    [SerializeField]
    private Transform endTransform;

    [SerializeField]
    private float movementTimeInSeconds = 5f;

    private IEnumerator Start()
    {
        float t = 0f;

        while (t < 1f)
        {
            float animationCurveTime = animationCurve.Evaluate(t);

            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, animationCurveTime);

            t += Time.deltaTime / movementTimeInSeconds;
            yield return null;
        }

    }
}
