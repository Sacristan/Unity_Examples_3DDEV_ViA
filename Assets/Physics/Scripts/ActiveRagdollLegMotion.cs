using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollLegMotion : MonoBehaviour
{
    [SerializeField] bool mirror = false;
    [SerializeField] Transform legReference;

    HingeJoint _hingeJoint;

    private void Start()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        JointSpring jointSpring = _hingeJoint.spring;

        jointSpring.targetPosition = legReference.localEulerAngles.x;

        if (jointSpring.targetPosition > 180)
            jointSpring.targetPosition = jointSpring.targetPosition - 360;

        jointSpring.targetPosition = Mathf.Clamp(jointSpring.targetPosition, _hingeJoint.limits.min + 5, _hingeJoint.limits.max + 5);

        if (mirror) jointSpring.targetPosition *= -1;

        _hingeJoint.spring = jointSpring;
    }
}
