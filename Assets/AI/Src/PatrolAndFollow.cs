using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolAndFollow : MonoBehaviour
{
    private enum AIStatus { None, Patrol, Follow }

    [Range(0f, 1f)] [SerializeField] private float closeEnoughDistance = 0.3f;
    [Range(1f, 100f)] [SerializeField] private float maxSightDistance = 30f;
    [Range(1f, 360f)] [SerializeField] private float fieldOfViewAnglePatrol = 30f;
    [Range(1f, 360f)] [SerializeField] private float fieldOfViewAngleFollow = 180f;
    [Range(0f, 30f)] [SerializeField] private float loseInterestTime = 5f;
    [Range(0f, 30f)] [SerializeField] private float waypointIdleTime = 3f;

    [SerializeField] Transform sightOrigin;
    [SerializeField] LayerMask sightLayerMask = ~0;

    private AIStatus aiStatus = AIStatus.None;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;
    private Waypoint[] _waypoints;
    Animator _animator;

    private uint currentWaypointIndex = 0;
    private Vector3 destinationPosition;
    private float distanceToDestination;
    private float accumulatedLoseInterestRate = 0f;

    private Coroutine currentStateRoutine;

    private AIStatus CurrentAIStatus
    {
        get => aiStatus;

        set
        {
            if (aiStatus != value)
            {
                aiStatus = value;
                UpdateStatus();
            }
        }
    }

    private float FieldOfViewAngle
    {
        get
        {
            switch (CurrentAIStatus)
            {
                case AIStatus.Follow:
                    return fieldOfViewAngleFollow;
                case AIStatus.Patrol:
                    return fieldOfViewAnglePatrol;
                default:
                    throw new System.Exception("Unsupported AiStatus: " + CurrentAIStatus);
            }
        }
    }

    public Vector3 PlayerPosForRaycast => _player.transform.position + Vector3.up * 0.5f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _waypoints = FindObjectsOfType<Waypoint>();

        _animator = GetComponentInChildren<Animator>();

        CurrentAIStatus = AIStatus.Patrol;

        StartCoroutine(SightRoutine());
    }

    private void UpdateStatus()
    {
        Debug.Log("STATUS changed to: " + CurrentAIStatus);

        SetWalk(true);


        if (currentStateRoutine != null) StopCoroutine(currentStateRoutine);

        switch (CurrentAIStatus)
        {
            case AIStatus.Patrol:
                UpdateTargetWaypointDestination();
                UpdateNavMeshAgentDestinationPosition();
                currentStateRoutine = StartCoroutine(PatrolRoutine());
                break;
            case AIStatus.Follow:

                currentStateRoutine = StartCoroutine(FollowRoutine());

                break;
        }
    }

    /// <summary>
    /// Tests if player can be seen
    /// </summary>
    /// <returns></returns>
    private bool CanSeePlayer()
    {
        Vector3 direction = PlayerPosForRaycast - sightOrigin.position;

        //Check if raycast can hit player (no obstacles in between)
        if (Physics.Raycast(sightOrigin.position, direction, out RaycastHit hit, maxSightDistance, sightLayerMask))
        {
            // Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Player") //check if other object tag is Player to avoid conflicts with scene geometry
            {
                if ((Vector3.Angle(direction, sightOrigin.forward)) <= FieldOfViewAngle) return true;
            }
        }

        return false;
    }

    private IEnumerator SightRoutine()
    {
        YieldInstruction waitInstruction = new WaitForSeconds(0.1f);

        while (true)
        {
            CalcDistance();
            if (CurrentAIStatus == AIStatus.Patrol && CanSeePlayer()) CurrentAIStatus = AIStatus.Follow;
            yield return waitInstruction;
        }
    }

    private IEnumerator FollowRoutine()
    {
        while (true)
        {
            bool canSeePlayer = CanSeePlayer();
            bool closeEnoughToDestination = distanceToDestination <= closeEnoughDistance;

            if (canSeePlayer)
            {
                //Can see player - update position
                if (!closeEnoughToDestination)
                {
                    destinationPosition = _player.transform.position;
                    UpdateNavMeshAgentDestinationPosition();
                }

                accumulatedLoseInterestRate = 0f;
            }
            else
            {
                if (closeEnoughToDestination)
                {
                    accumulatedLoseInterestRate += Time.deltaTime;
                    if (accumulatedLoseInterestRate >= loseInterestTime) CurrentAIStatus = AIStatus.Patrol;
                }
            }


            yield return null;
        }
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (distanceToDestination <= closeEnoughDistance)
            {
                SetWalk(false);
                yield return new WaitForSeconds(waypointIdleTime);
                SetWalk(true);

                MoveNextWaypoint();
                UpdateTargetWaypointDestination();
                UpdateNavMeshAgentDestinationPosition();
            }

            yield return null;
        }
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

        for (uint i = 0; i < _waypoints.Length; i++)
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

    private void CalcDistance()
    {
        Vector3 a = transform.position;
        Vector3 b = destinationPosition;

        a.y = 0;
        b.y = 0;

        distanceToDestination = Vector3.Distance(a, b);
    }

    private void SetWalk(bool flag)
    {
        _animator?.SetBool("IsWalking", flag);
    }

}
