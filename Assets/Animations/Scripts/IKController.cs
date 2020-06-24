using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    Animator _animator;
    [SerializeField] Transform lookAtTarget;

    [SerializeField] Transform rightHandHoldTransform;
    [SerializeField] Transform leftHandHoldTransform;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(lookAtTarget.position);

        SetIK(AvatarIKGoal.RightHand, rightHandHoldTransform);
        SetIK(AvatarIKGoal.LeftHand, leftHandHoldTransform);

    }

    private void SetIK(AvatarIKGoal ikGoal, Transform target)
    {
        _animator.SetIKPositionWeight(ikGoal, 1);
        _animator.SetIKRotationWeight(ikGoal, 1);

        _animator.SetIKPosition(ikGoal, target.position);
        _animator.SetIKRotation(ikGoal, target.rotation);
    }
}
