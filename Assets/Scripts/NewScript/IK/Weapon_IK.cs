using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IK : MonoBehaviour
{
    public float weight = .5f;

    public Animator animator;
    public CharacterController characterController;
    public Arsenal_Transform Arsenal_Transform;
    public Transform target_look;

    public Transform l_hand;
    public Transform l_hand_target;
    public Transform r_hand;

    public Quaternion left_hand_rotation;

    public float rh_weight;

    public Transform shoulder;
    public Transform aim_pivot;
    // Start is called before the first frame update
    void Start()
    {
        Arsenal_Transform = GetComponent<Arsenal_Transform>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aim_pivot = new GameObject().transform;
        aim_pivot.name = "aim_pivot";
        aim_pivot.transform.parent = transform;
        Debug.Log(transform.position);

        r_hand = new GameObject().transform;
        r_hand.name = "riht_hand";
        r_hand.transform.parent = aim_pivot;

        l_hand = new GameObject().transform;
        l_hand.name = "left_hand";
        l_hand.transform.parent = aim_pivot;

        r_hand.localPosition = Arsenal_Transform.WaterGun.rHandPos;
        Vector3 watergun_rotation = Arsenal_Transform.WaterGun.rHandRot;
        Quaternion rotRight = Quaternion.Euler(watergun_rotation.x, watergun_rotation.y, watergun_rotation.z);
        r_hand.localRotation = rotRight;
    }

    void Update()
    {
        if (GameObject.Find("leftHandPos"))
            l_hand_target = GameObject.Find("leftHandPos").transform;
        else
            return;
        left_hand_rotation = l_hand_target.rotation;
        l_hand.position = l_hand_target.position;
        if (animator.GetBool("Aiming"))
            rh_weight += Time.deltaTime*3;
        else
            rh_weight -= Time.deltaTime*3;
        rh_weight = Mathf.Clamp(rh_weight, 0, 1);
    }

    void OnAnimatorIK()
    {
        aim_pivot.position = shoulder.position;
        if (!animator.GetBool("IsSword"))
        {
            if (animator.GetBool("Aiming"))
            {
                aim_pivot.LookAt(target_look);
                animator.SetLookAtWeight(1, weight, weight);
            }
            else
            {
                animator.SetLookAtWeight(weight, 0, weight);
            }
            animator.SetLookAtPosition(target_look.position);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, l_hand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, left_hand_rotation);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_weight);
            animator.SetIKPosition(AvatarIKGoal.RightHand, r_hand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, r_hand.rotation);
        }
        else
            return;
    }
}
