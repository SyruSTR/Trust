﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_Controller : MonoBehaviour {

    public float offset;
    public bool isActiveIK = true;
    public float height;
    

    private Animator animator;

    private Vector3 leftFootTargetPos;
    private Vector3 rightFootTargetPos;

    private Quaternion leftFootTargetRot;
    private Quaternion rightFootTargetRot;

    private float leftFootWeight;
    private float rightFootWeight;

    private Transform leftFoot;
    private Transform rightFoot;

    public bool isGround;

	void Start () {
        animator = GetComponent<Animator>();

        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
	}
	
	void Update () {

        if (!isActiveIK)
            return;

        isGround = true;

        CheckFootPosition(leftFoot.position, ref leftFootTargetPos, ref leftFootTargetRot);
        CheckFootPosition(rightFoot.position, ref rightFootTargetPos, ref rightFootTargetRot);
	}
    void CheckFootPosition(Vector3 _footCurrentPosition, ref Vector3 _footNextPosition, ref Quaternion _footNextRotation)
    {
        Vector3 footPosition = _footCurrentPosition;
        RaycastHit hit;
        if (Physics.Raycast(footPosition + Vector3.up * height, Vector3.down, out hit, 1.5f))
        {
            _footNextPosition = Vector3.Lerp(footPosition, hit.point + Vector3.up * offset, Time.deltaTime * 10f);
            _footNextRotation = Quaternion.FromToRotation(transform.up, hit.normal);
        }
        else
            isGround = false;
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!isActiveIK)
            return;

        if (!isGround)
            return;

        float leftWeight = animator.GetFloat("leftFoot");
        float rightWeight = animator.GetFloat("rightFoot");

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);

        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTargetPos);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTargetRot);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTargetPos);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTargetRot);
    }
}