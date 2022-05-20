using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FleeFromPlayerNavmesh : MonoBehaviour
{
    [Range(0f, 50f)]
    [SerializeField]
    private float safeEnoughDistance = 5f;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < safeEnoughDistance)
        {
            PerformFleeFromPlayer();
        }
    }

    private void PerformFleeFromPlayer()
    {
        Vector3 direction = transform.position - _player.transform.position;
        Vector3 targetFleePos = transform.position + direction;

        if (NavMesh.SamplePosition(targetFleePos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            targetFleePos = hit.position;
        }

        _navMeshAgent.SetDestination(targetFleePos);
    }
}
