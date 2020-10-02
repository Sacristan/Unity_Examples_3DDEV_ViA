using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrapolatedMovement : MonoBehaviour
{
    Vector3 currentVelocity;

    CharacterController _characterController;

    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpTime = 2f;

    Camera _camera;

    Vector3 lastMovement;

    void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) ExtrapolateMotion();

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection = _camera.transform.rotation * movementDirection;

        Vector3 movement = movementDirection;
        movement *= movementSpeed;
        movement += Physics.gravity;
        movement *= Time.deltaTime;

        lastMovement = movement;

        ApplyMotion(movement, movementDirection);
    }

    private void ExtrapolateMotion()
    {
        ApplyMotion(lastMovement * (1f / Time.deltaTime) * jumpTime, lastMovement.normalized); //last movement * framerate * time
    }

    private void ApplyMotion(Vector3 movement, Vector3 movementDir)
    {
        _characterController.Move(movement);
        if (movementDir.sqrMagnitude > 0.01f) transform.forward = movementDir;
    }
}
