using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    const float PitchLimit = 89;

    private float movementSpeed = 50f;
    private float rotationSpeed = 100f;

    private CharacterController _characterController;

    [SerializeField]
    private float mouseSensitivity = 1.5f;

    private float pitch;
    private float yaw;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        pitch = transform.root.eulerAngles.x;
        yaw = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        yaw += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed * mouseSensitivity;

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