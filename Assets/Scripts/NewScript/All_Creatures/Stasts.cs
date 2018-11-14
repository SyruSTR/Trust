using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasts : Singleton<Stasts>
{
    protected Stasts() { }
    private int MaxHealt = 100;
    private bool attack_Trigger;
    private bool sword_trigger;
    private float weak_attack_count;
    private float large_attack;

    public int Health
    {
        get { return MaxHealt; }
    }
    public bool Attack_Trigger
    {
        get { return attack_Trigger; }
        set { attack_Trigger = value; }
    }
    //public bool Sword_Trigger
    //{
    //    get { return sword_trigger; }
    //    set { sword_trigger = value; }
    //}
    public float Weak_attack_count
    {
        get {return weak_attack_count; }
        set { weak_attack_count = value; }
    }
    public float Large_attack
    {
        get { return large_attack; }
        set { large_attack = value; }
    }
    public bool Weak_Attack_NOT_COUNT
    {
        get; set;
    }
    public bool Large_Attack_NOT_COUNT
    {
        get; set;
    }


    void Update()
    {
        //Debug.Log(attack_Trigger + "Attack");
        //Debug.Log(sword_trigger + "Sword");
        if (!attack_Trigger && sword_trigger)
        {
            SetDamage(10);
            attack_Trigger = true;
        }
        //if (!sword_trigger) attack_Trigger = false;
    }
    public void SetDamage(int damage)
    {
        MaxHealt -= damage;
        if (MaxHealt < 0)
        {
            MaxHealt = 0;
            //TODO: Add dead logic
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword_Skeleton"))
        {
            //Debug.Log("true");
            sword_trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword_Skeleton"))
        {
            //Debug.Log("false");
            sword_trigger = false;
        }
    }
}
