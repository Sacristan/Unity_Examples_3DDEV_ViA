using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWander : MonoBehaviour
{
    NavMeshAgent _navmeshAgent;

    [SerializeField] private float wanderRange = 10f;
    [SerializeField] private float closeEnoughDistance = 0.5f;

    [SerializeField] private float minIdleTime = 2f;

    [SerializeField] private float maxIdleTime = 5f;

    private Vector3 currentTarget;

    void Start()
    {
        _navmeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        WanderToPos();

        while (true)
        {
            if (Vector3.Distance(transform.position, currentTarget) <= closeEnoughDistance)
            {
                yield return new WaitForSeconds(Random.Range(minIdleTime, maxIdleTime));
                WanderToPos();
            }

            yield return null;
        }
    }

    void WanderToPos()
    {
        if (RandomPoint(transform.position, wanderRange, out Vector3 wanderPos))
        {
            _navmeshAgent.SetDestination(wanderPos);
            currentTarget = wanderPos;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
