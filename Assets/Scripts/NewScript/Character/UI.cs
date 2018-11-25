using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private GameObject stats;
    private Stats stats_script;
    public Slider slider;
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player");
        stats_script = GetComponent<Stats>();
        slider.value = stats_script.Health;
        slider.maxValue = stats_script.Health;
    }

    void Update()
    {
        slider.value = stats_script.Health;
        //Debug.Log(Stasts.Instance.Health.ToString());
    }
}
