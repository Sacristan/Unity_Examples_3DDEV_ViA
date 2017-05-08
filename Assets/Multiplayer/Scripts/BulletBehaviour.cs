using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject Origin { get; set; }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag == "Player")
        {
            if (hit == Origin) return;

            Health health = hit.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(10);
            }

            Destroy(gameObject);
        }
    }
}