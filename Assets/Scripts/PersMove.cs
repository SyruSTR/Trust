using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersMove : MonoBehaviour {

    float speed = 2f;
    float gravity = 20f;

    Vector3 direction;
    CharacterController controller;

	void Start () {
        controller = GetComponent<CharacterController>();
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
	}
}
