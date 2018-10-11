using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr : MonoBehaviour {
    Animator animator;

    void Start () {
        animator = GetComponent<Animator>();
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetBool("Attack", true);
            animator.SetFloat("attack", 1);
            Debug.Log("gg");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //animator.SetBool("Attack", false);
            animator.SetFloat("attack", 0);
            Debug.Log("123");
        }
    }
}
