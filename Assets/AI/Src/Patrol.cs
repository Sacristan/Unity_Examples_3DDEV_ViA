using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float closeEnoughDistance = 0.1f;
    [Range(0f, 30f)] [SerializeField] private float waypointIdleTime = 3f;

    [SerializeField] Waypoint[] waypoints;

    private int currentWaypointIndex = 0;

    private Waypoint CurrentWaypoint => waypoints[currentWaypointIndex];

    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        UpdateDestination();

        while (true)
        {
            float distanceToDestination = Vector3.Distance(transform.position, CurrentWaypoint.transform.position);

            if (distanceToDestination <= closeEnoughDistance)
            {
                yield return new WaitForSeconds(waypointIdleTime);

                IncrementWaypoint();
                UpdateDestination();
            }

            yield return null;
        }
    }

    private void IncrementWaypoint()
    {
        if (++currentWaypointIndex >= waypoints.Length) currentWaypointIndex = 0;
    }

    private void UpdateDestination()
    {
        _navMeshAgent.SetDestination(CurrentWaypoint.transform.position);
    }
}
