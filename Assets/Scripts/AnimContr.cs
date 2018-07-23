using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour {


    Animator animator;
    float vertical;
    //float horizontal;
    bool Test_run;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        vertical = Input.GetAxis("Vertical");
        //horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
             Test_run = true;
        }
        if (!Input.GetKey(KeyCode.W))
        {
             Test_run = false;
        }
        if (!Test_run)
        {
            vertical = 0;
        }
        if (vertical == 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("RunBack",false);
            if (!animator.GetBool("Run"))
                Debug.Log("false");
        }
        if(vertical > 0)
        {
            animator.SetBool("Run", true);
            if (animator.GetBool("Run"))
                Debug.Log("true");
        }
        if (vertical < 0)
        {
            animator.SetBool("RunBack",true);
            if (animator.GetBool("RunBack"))
                Debug.Log("GGG");
        }
        
	}
}
