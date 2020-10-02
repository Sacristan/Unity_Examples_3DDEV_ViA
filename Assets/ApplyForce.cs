using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField] Rigidbody[] rigidbodies;
    [SerializeField] float forceStrength = 20f;

    [SerializeField] ForceMode forceMode = ForceMode.Impulse;

    private void Start()
    {
        StartCoroutine(ApplyForces());
    }

    IEnumerator ApplyForces()
    {
        yield return new WaitForSeconds(2f);

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddForce(RandomDir() * forceStrength, forceMode);
        }
    }

    private Vector3 RandomDir()
    {
        return new Vector3(
            (Random.value > 0.5f ? 1 : -1),
            (Random.value > 0.5f ? 1 : -1),
            (Random.value > 0.5f ? 1 : -1)
        );
    }
}
