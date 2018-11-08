using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        slider.value = Stasts.Instance.Health;
        slider.maxValue = Stasts.Instance.Health;
    }

    void Update()
    {
        slider.value = Stasts.Instance.Health;
        //Debug.Log(Stasts.Instance.Health.ToString());
    }
}
