using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    Arsenal_Controller Arsenal_Controller;
    private Animator animator;
    private bool isSword;
    private int i;//count

    void Start()
    {
        animator = GetComponent<Animator>();
        Arsenal_Controller = GetComponent<Arsenal_Controller>();
        isSword = true;
        i = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSword = !isSword;
            if (i > Arsenal_Controller.arsenal.Count -1)
            {
                i = 0;
            }
            //Debug.Log(i.ToString());
            string name = Arsenal_Controller.arsenal[i].name;
            Arsenal_Controller.SetArsenal(name);
            animator.SetBool("IsSword", isSword);
            i++;            
        }
    }
}
