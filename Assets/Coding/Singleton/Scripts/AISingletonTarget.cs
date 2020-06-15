using UnityEngine;

public class AISingletonTarget : MonoBehaviour
{
    private float closeEnoughDistance = 1f;

    [SerializeField]
    private float speed = 10f;

    void Update()
    {
        if (GameManagerSingleton.Target != null)
        {
            Vector3 currentPos = transform.position;
            Vector3 targetPos = GameManagerSingleton.Target.position;

            Vector3 direction = targetPos - transform.position;
            direction.Normalize();

            float distance = Vector3.Distance(currentPos, targetPos);

            if (distance > closeEnoughDistance)
            {
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }


}
