using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Range(0f, 50f)]
    [SerializeField]
    private float movementSpeed = 5f;

    [Range(0f, 10f)]
    [SerializeField]
    private float closeEnoughDistance = 1f;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
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
        Vector3 direction = _player.transform.position - transform.position; // get the direction from me to player
        direction.Normalize();  //normalize direction ( values -> (0..1) )

        transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
