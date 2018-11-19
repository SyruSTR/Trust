using System.Collections;
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
    private Animator animator;
    private float currentVelocity;
    NavMeshAgent nav;
    public float rotationOffset=1f;
    //private Transform target;
    //public string targetTag = "Palyer";

    void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        //target = GameObject.FindGameObjectWithTag(targetTag).transform;
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
            SideRay();
        }
        if (distance < 1.5f)
        {
            nav.enabled = false;
            animator.SetBool("isMove", false);
            animator.SetTrigger("Attack");

            //Right ray
            
        }
        else
            nav.speed = 3.5f;
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
            j += angle * Mathf.Deg2Rad/2;
            Vector3 dir =transform.TransformDirection( new Vector3(x,0,y));
            //RightRay
            GetRays(dir,i,true);
            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                //LeftRay
                GetRays(dir,i,false);
            }
        }
        
    }
    void GetRays(Vector3 dir,int Count,bool isRight)
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
                Debug.Log("Goood!!!");
                float targetRotation = (isRight? angle: -angle)* rotationOffset + transform.eulerAngles.y;
                Debug.Log(transform.eulerAngles.y + "\n" + targetRotation);
                if (Count != 0)
                transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
                //Debug.DrawLine(pos, hit.point, Color.green);
            }            
        }
        else
        {
            //Debug.Log("Путь преграждает: " + hit.collider.name);
            //Debug.DrawRay(pos, dir * 3f, Color.red);
        }
    }
}
