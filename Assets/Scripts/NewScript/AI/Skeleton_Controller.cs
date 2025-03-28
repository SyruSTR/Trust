﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton_Controller : MonoBehaviour
{
    public GameObject player;
    public float radius;
    public float distance;
    public Transform Head;
    public float rayRotation;
    public float angle;
    public float smoothTime = 0.1f;
    public float attack_radius=1.5f;
    private Animator animator;
    private float currentVelocity;
    NavMeshAgent nav;
    public float rotationOffset = 1f;
    public float offset;
    //private Transform target;
    //public string targetTag = "Palyer";

    void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("test_2");
        //target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
    public Animator anim_con
    {
        get { return animator; }
        set { animator = value; }
    }
    public float curve_a
    {
        get { return animator.GetFloat("Attack_Curve"); }
    }
    void Update()
    {
        bool attack = false;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (!attack)
        {
            nav.enabled = true;
            animator.SetBool("isMove", true);
            nav.SetDestination(player.transform.position);
        }
        //if (distance > radius)
        //{
        //    //nav.enabled = false;
        //    //animator.SetBool("isMove", false);
        //}
        if (distance < radius)
        {
            //nav.enabled = true;
            
            
            SideRay();
        }
        if (distance < attack_radius)
        {
            attack = true;
            nav.enabled = false;
            animator.SetBool("isMove", false);
            animator.SetTrigger("Attack");
        }
        else
            attack = false;
        if (animator.GetBool("Death"))
            nav.enabled = false;

        
        //if (distance < 3f)
        //{

        //    transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(Head.transform.eulerAngles.y - transform.eulerAngles.y, transform.eulerAngles.y, ref currentVelocity, smoothTime);
        //}
        else
            nav.speed = 3.5f;
        
        //Debug.Log(transform.eulerAngles.y + "\n" + Head.transform.eulerAngles.y);
        //if (hit.collider != null)
        //{
        //    if (hit.collider.gameObject != player.transform.gameObject)
        //    {
        //        Debug.Log("Путь преграждает: " + hit.collider.name);
        //    }
        //    else
        //        Debug.Log("Goood!!!");
        //    //Debug.DrawLine(ray.origin,hit.point,Color.red);
        //}
    }
    private void SideRay()
    {
        float j = 0;
        for (int i = 0; i < 2; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);
            j += angle * Mathf.Deg2Rad / 2;
            RaysMachine(x, y);
            //Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
            ////RightRay
            //GetRays(dir, i, true);
            //if (x != 0)
            //{
            //    dir = transform.TransformDirection(new Vector3(-x, 0, y));
            //    //LeftRay
            //    GetRays(dir, i, false);
            //}
        }
        void RaysMachine(float x, float y)
        {
            for (int i = 0; i < 2; i++)
            {
                y *= i < 0 ? 1 : -1;
                bool side = y>0 ? true : false;
                Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
                GetRays(dir, i, true,side);
                if (x != 0)
                {
                    dir = transform.TransformDirection(new Vector3(-x, 0, y));
                    //LeftRay
                    GetRays(dir, i, false,side);
                }
            }
            void GetRays(Vector3 dir, int Count, bool isRight,bool side)
            {
                Vector3 pos = Head.transform.position;
                Ray ray = new Ray(Head.transform.position, dir);
                //if(isRight)
                RaycastHit hit = new RaycastHit();
                //Debug.Log(dir);
                //Debug.DrawLine(ray.origin, hit.normal, Color.blue);
                if (Physics.Raycast(ray, out hit, 5f))
                {
                    if (hit.collider.gameObject == player.transform.gameObject)
                    {
                        //Debug.Log("Goood!!!");
                        float targetRotation = (isRight ? angle : -angle) * rotationOffset + transform.eulerAngles.y;
                        //Debug.Log(transform.eulerAngles.y + "\n" + targetRotation);
                        if (Count != 0)
                            transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
                        if(!side)
                            transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(transform.eulerAngles.y+offset, targetRotation, ref currentVelocity, smoothTime);
                        Debug.DrawLine(pos, hit.point, Color.green);
                    }
                }
                else
                {
                    //Debug.Log("Путь преграждает: " + hit.collider.name);
                    Debug.DrawRay(pos, dir * 3f, Color.red);
                }
            }
        }
    }
}