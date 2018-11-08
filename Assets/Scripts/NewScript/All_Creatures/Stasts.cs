using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stasts : Singleton<Stasts>
{
    protected Stasts() { }
    private int MaxHealt = 100;
    private bool attack_trigger;


    public void SetDamage(int damage)
    {
        MaxHealt -= damage;
        if (MaxHealt < 0)
        {
            MaxHealt = 0;
            //TODO: Add dead logic
        }
    }

    public int Health
    {
        get { return MaxHealt; }
    }
    public bool Attack_Trigger
    {
        get { return attack_trigger; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword_Skeleton"))
            attack_trigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword_Skeleton"))
            attack_trigger = false;
    }
}
