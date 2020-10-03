using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayerNavmeshObstacle : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField]
    private float closeEnoughDistance = 1f;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;

    private Transform Target => _player.transform;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Target.position) > closeEnoughDistance)
        {
            PerformFollowPlayer();
        }
    }

    private void PerformFollowPlayer()
    {
        _navMeshAgent.SetDestination(Target.position);
    }

    protected bool CanReachTarget()
    {
        Vector3 dir = (Target.position - transform.position).normalized;

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 50f))
        {
            return hit.transform == Target;
        }

        return false;
    }
}
