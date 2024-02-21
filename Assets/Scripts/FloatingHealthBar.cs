using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = 1 - currentValue / maxValue;
    }
   
}
