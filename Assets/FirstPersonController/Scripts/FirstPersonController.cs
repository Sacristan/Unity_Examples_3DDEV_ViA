using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    const float PitchLimit = 89;

    private float movementSpeed = 50f;
    private float rotationSpeed = 150f;

    private CharacterController _characterController;

    private float pitch;
    private float yaw;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        yaw += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;

        if (pitch > PitchLimit) pitch = PitchLimit;
        if (pitch < -PitchLimit) pitch = -PitchLimit;

        Vector3 movementVector = new Vector3(
            hInput,
            Physics.gravity.y,
            vInput
        );

        movementVector = Quaternion.Euler(Vector3.up * yaw) * movementVector;
        movementVector *= Time.deltaTime * movementSpeed;

        Rotate();
        Move(movementVector);
    }

    private void Move(Vector3 movementVector)
    {
        _characterController.Move(movementVector);
    }

    private void Rotate()
    {
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

}