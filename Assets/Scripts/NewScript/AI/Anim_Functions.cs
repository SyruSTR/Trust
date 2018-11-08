using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Functions : MonoBehaviour
{
    public void Damage()    
    {        
        //Debug.Log(ui.ToString());
        if(Stasts.Instance.Attack_Trigger)
        Stasts.Instance.SetDamage(10);
        
    }
}
