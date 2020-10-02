using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMovement : MonoBehaviour
{
    CharacterController _characterController;

    [SerializeField] float movementSpeed = 10f;

    Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection = _camera.transform.rotation * movementDirection; //TO translate to camera direction

        Vector3 movement = movementDirection;
        movement *= movementSpeed; //apply movement speed
        movement += Physics.gravity; //apply gravity vector
        movement *= Time.deltaTime; //make sure this works the same on different framerates

        _characterController.Move(movement);

        if (movementDirection.sqrMagnitude > 0.01f) transform.forward = movementDirection; // Rotate towards movement direction if theres any input
    }
}
