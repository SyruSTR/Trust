using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Functions : MonoBehaviour
{
    GameObject stats;
    Stats stats_script;
    //Skeleton Events
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player");
        stats_script = GetComponent<Stats>();
    }
    public void Skeleton_Damage()
    {
        FindPlayer().GetComponent<Stats>().Attack_Trigger = false;

    }
    //Knight Enents
    public void Damage()
    {
        foreach (GameObject skeleton in stats_script.FindSkeletons())
            skeleton.GetComponent<Skeleton_Stats>().Attack_Trigger = false;
    }
    //-----------
    public void Weak_Attack_Non_Count()
    {
        stats_script.Weak_Attack_NOT_COUNT = true;
        stats_script.Weak_attack_count = 0;
    }
    public void Weak_Attack()
    {
        stats_script.Weak_Attack_NOT_COUNT = false;
        stats_script.Weak_attack_count = 0;
    }
    //---------------
    public void Large_Attack_Non_Count()
    {
        stats_script.Large_Attack_NOT_COUNT = true;
        stats_script.Large_attack = 0;
    }
    public void Large_Attack()
    {
        stats_script.Large_attack = 0;
        stats_script.Large_Attack_NOT_COUNT = false;
    }
    public void Skeleton_Death()
    {
        //foreach (GameObject skeleton in stats_script.FindSkeletons())
            Destroy(this.gameObject);
    }
    public void Player_Death()
    {
        //Destroy(GameObject.Find("test_2"));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    private GameObject FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }
}
