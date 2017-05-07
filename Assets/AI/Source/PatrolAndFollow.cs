using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolAndFollow : MonoBehaviour
{
    private enum AIStatus { Patrol, Follow }

    private const float CloseEnoughWaypointDistance = 0.5f;

    [Range(0f, 1f)]
    [SerializeField]
    private float closeEnoughFollowDistance = 0.5f;

    [Range(1f, 100f)]
    [SerializeField]
    private float maxSightDistance = 10f;

    [Range(1f, 360f)]
    [SerializeField]
    private float fieldOfViewAnglePatrol = 30f;

    [Range(1f, 360f)]
    [SerializeField]
    private float fieldOfViewAngleFollow = 180f;

    [Range(0f, 30f)]
    [SerializeField]
    private float loseInterestTime = 5f;

    private AIStatus aiStatus = AIStatus.Patrol;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private Waypoint[] _waypoints;

    private uint currentWaypointIndex = 0;
    private Vector3 destinationPosition;
    private float distanceToDestination;
    private float accumulatedLoseInterestRate = 0f;

    private float FieldOfViewAngle
    {
        get
        {
            switch (aiStatus)
            {
                case AIStatus.Follow:
                    return fieldOfViewAngleFollow;
                case AIStatus.Patrol:
                    return fieldOfViewAnglePatrol;
                default:
                    throw new System.Exception("Unsupported AiStatus: "+aiStatus);
            }
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypoints = FindObjectsOfType<Waypoint>();

        UpdateTargetWaypointDestination();
        UpdateNavMeshAgentDestinationPosition();
    }

    private void Update()
    {
        distanceToDestination = Vector3.Distance(transform.position, destinationPosition);

        switch (aiStatus)
        {
            case AIStatus.Patrol:
                PerformPatrol();
                break;
            case AIStatus.Follow:
                PerformFollow();
                break;
        }

    }

    /// <summary>
    /// Tests if player can be seen
    /// </summary>
    /// <returns></returns>
    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 direction = _player.transform.position - transform.position;

        //Check if raycast can hit player (no obstacles in between)
        if (Physics.Raycast(transform.position, direction, out hit, maxSightDistance))
        {
            if (hit.transform.tag == "Player") //check if other object tag is Player to avoid conflicts with scene geometry
            {
                //Check if direction is within FOV angle
                if ((Vector3.Angle(direction, transform.forward)) <= FieldOfViewAngle) return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Follow Player. If not player not seen - move to last known pos
    /// </summary>
    private void PerformFollow()
    {
        bool canSeePlayer = CanSeePlayer();
        bool closeEnoughToDestination = distanceToDestination <= closeEnoughFollowDistance;

        if (canSeePlayer)
        {
            //Can see player - update position

            destinationPosition = _player.transform.position;
            UpdateNavMeshAgentDestinationPosition();

            accumulatedLoseInterestRate = 0f;
        }
        else
        {
            if (closeEnoughToDestination)
            {
                accumulatedLoseInterestRate += Time.deltaTime;
                if (accumulatedLoseInterestRate >= loseInterestTime) ResumePatrolling();
            }
        }
    }

    /// <summary>
    /// Perform patrolling between points
    /// </summary>
    private void PerformPatrol()
    {
        //If player has been seen while patrolling - start to follow
        if (CanSeePlayer()) StartFollowing();

        if (distanceToDestination <= CloseEnoughWaypointDistance)
        {
            MoveNextWaypoint();
            UpdateTargetWaypointDestination();
            UpdateNavMeshAgentDestinationPosition();
        }
    }

    private void StartFollowing()
    {
        Debug.Log("Spotted player while patrolling! Commence follow!");
        aiStatus = AIStatus.Follow;
    }

    private void ResumePatrolling()
    {
        Debug.Log("AI lost interest - resuming patrol");
        aiStatus = AIStatus.Patrol;
        FindClosestWaypoint();
        UpdateNavMeshAgentDestinationPosition();
    }

    private void UpdateNavMeshAgentDestinationPosition()
    {
        _navMeshAgent.SetDestination(destinationPosition);
    }

    #region Patrol Methods

    private void FindClosestWaypoint()
    {
        uint closestIndex = 0;
        float foundMinDistance = float.MaxValue;

        for(uint i=0; i<_waypoints.Length; i++)
        {
            Waypoint waypoint = _waypoints[i];
            float dist = Vector3.Distance(transform.position, waypoint.transform.position);

            if (dist < foundMinDistance)
            {
                foundMinDistance = dist;
                closestIndex = i;
            }
        }

        currentWaypointIndex = closestIndex;
    }

    private void MoveNextWaypoint()
    {
        if (++currentWaypointIndex >= _waypoints.Length) currentWaypointIndex = 0;
    }

    private void UpdateTargetWaypointDestination()
    {
        Waypoint waypoint = _waypoints[currentWaypointIndex];
        destinationPosition = waypoint.transform.position;
    }
    #endregion

}
