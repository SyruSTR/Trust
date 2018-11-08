using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersMove : MonoBehaviour {

    private float vertical;
    private float horizontal;
    public float speed = 2f;
    public float gravity = 20f;
    public float turnSpeed;
    public Transform respawn;
    public Transform posTarget;
    private bool death;

    Animator animator;


    Vector3 direction;
    CharacterController controller;

	void Start () {
        controller = GetComponent<CharacterController>();
        death = false;
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            PlayAnim(horizontal,vertical);
            direction = new Vector3(horizontal, 0f, vertical);
            direction = transform.TransformDirection(direction) * speed;
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    direction.y = 6;
            //}
        }
        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
        if (death)
        {
            transform.position = respawn.position;
        }

        if (horizontal != 0 || vertical != 0)
        {
            
            Vector3 dir = posTarget.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
        }
        
        //if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        //{
        //    speed = 3;
        //}
        //else
        //{
        //    speed = 4;
        //}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "deathZone")
        {
            death = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "deathZone")
        {
            death = false;
        }
    }

    public void PlayAnim(float horizontal, float vertical)
    {
        animator.SetFloat("vertical",vertical);
        animator.SetFloat("horizontal", horizontal);
    }
}
