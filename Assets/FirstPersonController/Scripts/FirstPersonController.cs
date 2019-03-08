using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    private const float PitchLimit = 89;
    private const float RotationSpeed = 100f;

    [SerializeField]
    private float movementSpeed = 25f;

    [SerializeField]
    private float mouseSensitivity = 1.5f;

    private CharacterController _characterController;

    private float pitch;
    private float yaw;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        pitch = transform.root.eulerAngles.x;
        yaw = transform.rotation.eulerAngles.y;

    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        yaw += Input.GetAxis("Mouse X") * Time.deltaTime * RotationSpeed * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * Time.deltaTime * RotationSpeed * mouseSensitivity;

        if (pitch > PitchLimit) pitch = PitchLimit;
        if (pitch < -PitchLimit) pitch = -PitchLimit;

        Vector3 movementVector = new Vector3(
            hInput,
            0f,
            vInput
        );

        movementVector *= movementSpeed;
        movementVector.y = Physics.gravity.y;

        movementVector = Quaternion.Euler(Vector3.up * yaw) * movementVector; //converts global movementVector to local vector

        movementVector *= Time.deltaTime;

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