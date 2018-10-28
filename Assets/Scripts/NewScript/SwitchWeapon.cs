using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    Arsenal_Controller Arsenal_Controller;
    private int i;//count

    void Start()
    {
        Arsenal_Controller = GetComponent<Arsenal_Controller>();
        i = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (i > Arsenal_Controller.arsenal.Count -1)
            {
                i = 0;
            }
            //Debug.Log(i.ToString());
            string name = Arsenal_Controller.arsenal[i].name;
            Arsenal_Controller.SetArsenal(name);
            i++;

            
            
        }
    }
}
