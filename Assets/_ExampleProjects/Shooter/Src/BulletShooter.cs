using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float destroyTimeInSeconds = 2f;

    [SerializeField]
    private int damage;

    private IEnumerator Start()
    {
        Destroy(gameObject, destroyTimeInSeconds);

        while (true)
        {
            transform.position += transform.forward * Time.deltaTime * bulletSpeed;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable> ();

        if (damageable != null)
        {
            damageable.ApplyDamage(damage);
        }

        Destroy(this);
    }
}
