using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        print("Just collided with " + collision.collider.gameObject.name);
    }
    
    void OnTriggerEnter(Collider collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        print("Just triggered destroy in 3 seconds for " + colliderGameObject.name);
        Destroy(colliderGameObject, 3);
    }

}
