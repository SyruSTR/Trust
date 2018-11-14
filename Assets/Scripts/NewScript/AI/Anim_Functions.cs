using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Functions : MonoBehaviour
{
    //Skeleton Events
    public void Skeleton_Damage()
    {
        Stasts.Instance.Attack_Trigger = false;
            
    }
    //Knight Enents
    public void Damage()
    {
        Skeleton_Stats.Instance.Attack_Trigger = false;
    }
    //-----------
    public void Weak_Attack_Non_Count()
    {
        Stasts.Instance.Weak_Attack_NOT_COUNT = true;
        Stasts.Instance.Weak_attack_count=0;
    }
    public void Weak_Attack()
    {
        Stasts.Instance.Weak_Attack_NOT_COUNT = false;
        Stasts.Instance.Weak_attack_count = 0;
    }
    //---------------
    public void Large_Attack_Non_Count()
    {
        Stasts.Instance.Large_Attack_NOT_COUNT = true;
        Stasts.Instance.Large_attack=0;
    }
    public void Large_Attack()
    {
        Stasts.Instance.Large_attack = 0;
        Stasts.Instance.Large_Attack_NOT_COUNT = false;
    }
}
