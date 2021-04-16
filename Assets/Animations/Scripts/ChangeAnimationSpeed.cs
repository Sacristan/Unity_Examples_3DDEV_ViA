using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationSpeed : MonoBehaviour
{

    Animator _animator;

    float speed = 0.1f;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat("speed", Mathf.PingPong(Time.time * speed, 1f));
    }
}
