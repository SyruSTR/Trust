using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_Controller : MonoBehaviour
{
    public GameObject player;
    public float radius;
    public float distance;
    private Animator animator;
    NavMeshAgent nav;

    void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > radius)
        {
            nav.enabled = false;
            animator.SetBool("isMove", false);
        }
        if (distance < radius)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            animator.SetBool("isMove", true);
        }
        if(distance < 1.3)
        {
            nav.enabled = false;
            animator.SetBool("isMove", false);
            animator.SetTrigger("Attack");
        }
    }
}
