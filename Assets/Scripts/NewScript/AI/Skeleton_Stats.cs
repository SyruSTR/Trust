using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeleton_Stats : Singleton<Skeleton_Stats>
{
	protected Skeleton_Stats() {}
	public Slider slider;
	public Text text_HP;
	private int HP=100;
	private bool attack_Trigger;
	private bool sword_Trigger;

	void Update()
	{
		
		if (Input.GetKey(KeyCode.G))
		{
			HP = 100;
		}
		text_HP.text = HP.ToString();
		Debug.Log(sword_Trigger.ToString() + attack_Trigger.ToString());
		//Debug.Log(attack_Trigger + " Attack");
		//Debug.Log(sword_Trigger + " sword");
		if (!attack_Trigger && sword_Trigger)
		{
			SetDamage(10);
			attack_Trigger = true;
		}
		//if (!sword_Trigger) attack_Trigger = false;
		slider.value = HP;
		
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

	void Start()
	{
		slider.maxValue = HP;
		slider.value = HP;
	}
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
