using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNavmeshObstacle : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float moveBlockerTime = 4f;
    [SerializeField] float blockerMovementTime = 2f;


    void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(moveBlockerTime);

        Vector3 originalPos = transform.position;

        float t = 0;
        
        do
        {
            t += Time.deltaTime / blockerMovementTime;

            transform.position = Vector3.Lerp(originalPos, target.position, t);

            yield return null;
        } while (t < 1f);

    }

}
