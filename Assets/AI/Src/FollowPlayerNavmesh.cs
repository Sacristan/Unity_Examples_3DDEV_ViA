using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayerNavmesh : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField]
    private float closeEnoughDistance = 1f;

    private GameObject _player;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > closeEnoughDistance) // check if distance between player and gameobject is greater than close enough value
        {
            PerformFollowPlayer();
        }
    }

    /// <summary>
    /// Follows Player
    /// </summary>
    private void PerformFollowPlayer()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
