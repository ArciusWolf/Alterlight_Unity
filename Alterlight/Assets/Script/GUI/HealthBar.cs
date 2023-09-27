using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float easeSpeed = 10f;

    public void setHealth(float health)
    {
        healthSlider.value = health;
    }
    
    public void setMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void Update()
    {
        if (healthSlider.value != easeHealthSlider.value)
        {
        // slowly move the health bar to the correct value
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, Time.deltaTime * easeSpeed);
        }
    }

}