using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeFromPlayer : MonoBehaviour
{
    [Range(0f, 50f)]
    [SerializeField]
    private float movementSpeed = 5f;

    [Range(0f, 50f)]
    [SerializeField]
    private float safeEnoughDistance = 5f;

    [SerializeField]
    private bool allowYMovement = false;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Find player by tag. It can be assigned in game object inspector tag section (below name).
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
        Vector3 direction = transform.position - _player.transform.position; // direction away from player
        direction.Normalize();  //normalize direction ( values -> (0..1) )
        if (!allowYMovement) direction.y = 0; //if y movement is disallowed force direction y to be 0

        transform.position += direction * movementSpeed * Time.deltaTime;
    }
}
