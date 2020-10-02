using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBlendShape : MonoBehaviour
{
    float animationSpeed = 100f;

    SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Start()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        _skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.PingPong(Time.time * animationSpeed, 100f));
    }
}
