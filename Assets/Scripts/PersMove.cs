using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersMove : MonoBehaviour {

    public float speed = 2f;
    public float gravity = 20f;
    public Transform respawn;
    private bool death;

    Vector3 direction;
    CharacterController controller;

	void Start () {
        controller = GetComponent<CharacterController>();
        death = false;
	}
	
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            direction = new Vector3(x, 0f, z);
            direction = transform.TransformDirection(direction) * speed;
        }
        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
        if (death)
        {
            transform.position = respawn.position;
        }
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
}
