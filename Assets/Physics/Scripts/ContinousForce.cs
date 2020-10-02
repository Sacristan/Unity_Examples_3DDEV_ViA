using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinousForce : MonoBehaviour
{
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    [SerializeField] float movementForce = 5f; //5 newtons per second

    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.right * movementForce, forceMode);
    }
}
