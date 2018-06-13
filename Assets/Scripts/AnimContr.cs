using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour {


    Animator animator;
    float vertical;
    float horizontal;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (vertical == 0)
        {
            animator.SetBool("Run", false);
            Debug.Log("333");
        }
        if(vertical >= 0.1)
        {
            animator.SetBool("Run", true);
            Debug.Log("111");
        }
	}
}
