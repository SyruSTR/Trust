﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 3.5f;
    public float gravity = -9.8f;
    public float speedAnim;
    private float targetSpeed =0;

    [Space]
    public float smoothTime = 0.1f;
    private float currentSpeed;
    private float currentVelocitySpeed;

    private float currentVelocityRotate;

    [Space]
    public float jumpForce = 1.35f;
    public float jumpSpeedOffset = 0.7f;
    private float jumpVelocity = 0;


    //private bool
    private bool attack_weak;
    private bool attack_large;

    //Cash
    private CharacterController characterController;
    private Animator animator;
    public new Transform camera;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsSword", true);
        // camera = Camera.main.transform;
    }
    void Update()
    {
        animator.SetFloat("Attack_WeakF", Stasts.Instance.Weak_attack_count);
        animator.SetFloat("Attack_LargeF", Stasts.Instance.Large_attack);
        animator.SetBool("isGround", characterController.isGrounded);
        if(!characterController.isGrounded)
        {
            Stasts.Instance.Weak_attack_count = 0;
            Stasts.Instance.Large_attack = 0;
            Stasts.Instance.Weak_Attack_NOT_COUNT = false;
            Stasts.Instance.Large_Attack_NOT_COUNT = false;
        }
    }
    public void Move(Vector2 _input, bool _isAcceleration)
    {
        if (_input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocityRotate, smoothTime);            
            //Debug.Log("targetRotation" + targetRotation.ToString());
            //Debug.Log("transform.eulerAngles" + transform.eulerAngles.ToString());
        }
        
        if (_input.x != 0 || _input.y != 0 && !(_input.x != 0 && _input.y != 0))
        {
            targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * 1;
        }
        else
        {            
            targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * _input.magnitude;
        }
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);

        
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * gravity + Vector3.up * jumpVelocity;
        if (velocity.y > 0)
        {
            velocity.x *= currentSpeed * jumpSpeedOffset;
            velocity.z *= currentSpeed * jumpSpeedOffset;
        }

        characterController.Move(velocity * Time.deltaTime);

        if (jumpVelocity > 0)
            jumpVelocity += gravity * Time.deltaTime;
        else if (jumpVelocity < 0)
            jumpVelocity = 0;

        animator.SetBool("isMove", targetSpeed != 0);
        animator.SetFloat("movementSpeed", targetSpeed * speedAnim);
        
        if (characterController.isGrounded)
             animator.SetFloat("groudDistance", GetGroundDistance());
        //Debug.Log("Velocity" + velocity.ToString());
        //Debug.Log("currentSpeed" + currentSpeed.ToString());
        //Debug.Log("jumpVelocity" + jumpVelocity.ToString());
        //Debug.Log((targetSpeed * speedAnim).ToString());
        
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            jumpVelocity = -gravity * jumpForce;
            animator.SetTrigger("Jump");
        }
    }
    public void Attack(bool strange)
    {
        if (characterController.isGrounded)
        {
            if (strange && !Stasts.Instance.Weak_Attack_NOT_COUNT)
            {
                Stasts.Instance.Weak_attack_count++;                
            }
            else if (!strange && !Stasts.Instance.Weak_Attack_NOT_COUNT)
            {
                Stasts.Instance.Large_attack++;                
            }
        }
    }

    private float GetGroundDistance()
    {
        float downPoint = transform.position.y + characterController.center.y - characterController.height * 0.5f;
        Vector3 startPoint = transform.position;
        startPoint.y = downPoint + 0.1f;

        if (Physics.Raycast(startPoint, Vector3.down, out RaycastHit hit))
        {
            return (float)System.Math.Round(hit.distance, 2);
        }
        return float.MaxValue;
    }
}