using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolAndFollow : MonoBehaviour
{
    [SerializeField]
    private float closeEnoughWaypointDistance = 0.5f;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private Waypoint[] _waypoints;

    private uint currentWaypointIndex = 0;
    private Vector3 destinationPosition;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypoints = FindObjectsOfType<Waypoint>();

        UpdateTargetWaypointDestination();
        SetNavMeshAgentPos();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, destinationPosition);

        if(distance <= closeEnoughWaypointDistance)
        {
            MoveNextWaypoint();
            UpdateTargetWaypointDestination();
            SetNavMeshAgentPos();
        }
    }

    private void MoveNextWaypoint()
    {
        if (++currentWaypointIndex >= _waypoints.Length) currentWaypointIndex = 0;
    }

    private void SetNavMeshAgentPos()
    {
        _navMeshAgent.SetDestination(destinationPosition);
    }

    private void UpdateTargetWaypointDestination()
    {
        Waypoint waypoint = _waypoints[currentWaypointIndex];
        destinationPosition = waypoint.transform.position;
    }

}
