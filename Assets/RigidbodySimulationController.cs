using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodySimulationController : MonoBehaviour
{
    [SerializeField] float force = 20f;



    private void FixedUpdate()
    {
        foreach (var r in GetComponentsInChildren<Rigidbody>())
        {
            r.AddForce(Vector3.up * force, ForceMode.Acceleration);
        }

    }
}
