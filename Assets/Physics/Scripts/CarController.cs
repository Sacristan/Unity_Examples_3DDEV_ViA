using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    private class AxleInfo
    {
        [SerializeField] WheelCollider leftWheel;
        [SerializeField] WheelCollider rightWheel;
        [SerializeField] bool isMotor;
        [SerializeField] bool handlesSteering;

        public WheelCollider LeftWheel => leftWheel;
        public WheelCollider RightWheel => rightWheel;
        public bool IsMotor => isMotor;
        public bool HandlesSteering => handlesSteering;
    }

    [SerializeField] AxleInfo[] axleInfos;
    [SerializeField] float maxMotorTorque = 400;
    [SerializeField] float maxSteeringAngle = 30;

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        for (int i = 0; i < axleInfos.Length; i++)
        {
            AxleInfo axleInfo = axleInfos[i];

            if (axleInfo.HandlesSteering)
            {
                axleInfo.LeftWheel.steerAngle = steering;
                axleInfo.RightWheel.steerAngle = steering;
            }
            if (axleInfo.IsMotor)
            {
                axleInfo.LeftWheel.motorTorque = motor;
                axleInfo.RightWheel.motorTorque = motor;
            }

            ApplyLocalPositionToVisuals(axleInfo.LeftWheel);
            ApplyLocalPositionToVisuals(axleInfo.RightWheel);
        }
    }

    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) return;

        Transform visualWheel = collider.transform.GetChild(0);

        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

