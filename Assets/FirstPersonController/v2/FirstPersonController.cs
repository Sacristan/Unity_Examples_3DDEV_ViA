using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VIA.FPC.v2
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 10f;
        [SerializeField] float rotationSpeed = 5f;

        CharacterController _characterController;
        Camera _camera;

        float pitch;
        float jaw;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = GetComponentInChildren<Camera>();

            pitch = transform.localEulerAngles.x;
            jaw = transform.localEulerAngles.y;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float hKeyboard = Input.GetAxis("Horizontal");
            float vKeyboard = Input.GetAxis("Vertical");

            float hMouse = Input.GetAxis("Mouse X");
            float vMouse = Input.GetAxis("Mouse Y");

            jaw += hMouse * Time.deltaTime * rotationSpeed; // X Axis
            pitch -= vMouse * Time.deltaTime * rotationSpeed; //Y Axis

            Vector3 input = new Vector3(hKeyboard, 0, vKeyboard);

            Vector3 localInput = transform.rotation * input;
            Vector3 motion = localInput * movementSpeed * Time.deltaTime;

            motion += Physics.gravity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0, jaw, 0);
            _characterController.Move(motion);
        }

        private void LateUpdate()
        {
            _camera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

    }
}
