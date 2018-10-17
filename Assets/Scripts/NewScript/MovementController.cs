using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float runSpeed = 3.5f;
    public float gravity = -9.8f;

    [Space]
    public float smoothTime = 0.1f;
    private float currentSpeed;
    private float curentVelocitySpeed;

    private float currentVelocityRotate;

    [Space]
    public float jumpForce = 1.35f;
    private float jumpVelocity = 0;

    //Cash
    private CharacterController characterController;
    private Animator animator;
    private new Transform camera;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;
    }

    public void Move(Vector2 _input, bool _isAcceleration)
    {
        if(_input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref curentVelocitySpeed, smoothTime);

            //float targetSpped = 

        }
    }

    void Update()
    {
        
    }
}
