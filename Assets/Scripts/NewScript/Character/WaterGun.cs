using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    private GameObject water_tear;
    private Transform transform_spawn;
    private MovementController movementController;
    private Animator animator;
    public GameObject water_tear_prefab;
    public float speed_tear;
    void Start()
    {
        //transform_spawn = gameObject.transform;
        transform_spawn = GameObject.Find("Target").transform;
        animator = GameObject.Find("test_2").GetComponent<MovementController>().animator_p;
        //movementController = GameObject.Find("test_2").GetComponent<MovementController>();
    }


    void Update()
    {
        Debug.Log(animator.GetBool("IsSword")+"\n"+ animator.GetBool("Aiming"));
        if (animator.GetBool("IsSword") || !animator.GetBool("Aiming"))
        {
            Debug.Log("111");
            return;
        }
        else

        if (Input.GetKey(KeyCode.O /*&& !isSword*/))
        {
            //create water_tear;
            water_tear = Instantiate(water_tear_prefab, transform_spawn.position, Quaternion.identity);

        }
    }
}
