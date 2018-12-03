using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton_Stats : MonoBehaviour
{
    protected Skeleton_Stats() { }
    public Slider slider;
    public Text text_HP;
    public int damage;
    public int HP = 100;
    private bool attack_Trigger;
    private bool sword_Trigger;
    private GameObject movement_controller;
    private Stats stats;

    void Start()
    {
        movement_controller = GameObject.Find("test_2");
        slider.maxValue = HP;
        slider.value = HP;
        stats = GetComponent<Stats>();
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.G))
        {
            HP = 100;
        }
        text_HP.text = HP.ToString();
        //Debug.Log(sword_Trigger.ToString() + attack_Trigger.ToString());
        //Debug.Log(attack_Trigger + " Attack");
        //Debug.Log(sword_Trigger + " sword");
        float curve = movement_controller.GetComponent<MovementController>().curve_a;
        if (!attack_Trigger && sword_Trigger && curve > 0)
        {            
            SetDamage(movement_controller.GetComponent<MovementController>().Damage);
            attack_Trigger = true;
        }
        //if (!sword_Trigger) attack_Trigger = false;
        slider.value = HP;
        if (Health <= 0)
        {
            GameObject skeleton_1 = this.gameObject;
            //foreach(GameObject skeleton in movement_controller.GetComponent<Stats>().FindSkeletons())
            skeleton_1.GetComponent<Skeleton_Controller>().anim_con.SetBool("Death",true);
        }
    }

    public int Health
    {
        get { return HP; }
    }
    public bool Attack_Trigger
    {
        get { return attack_Trigger; }
        set { attack_Trigger = value; }
    }
    //public bool Sword_Trigger
    //{
    //    get { return sword_Trigger; }
    //    set { sword_Trigger = value; }
    //}

    
    public void SetDamage(int damage)
    {
        HP -= damage;
        slider.value = HP;
        if (HP < 0)
        {
            HP = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            //Debug.Log("true");
            sword_Trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            //Debug.Log("false");
            sword_Trigger = false;
        }
    }
}
