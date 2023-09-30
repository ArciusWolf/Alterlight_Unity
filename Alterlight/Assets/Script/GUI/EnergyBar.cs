using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energySlider;
    public Slider easeEnergySlider;
    private float easeSpeed = 10f;

    public void setEnergy(float mana)
    {
        energySlider.value = mana;
    }

    public void setMaxEnergy(float mana)
    {
        energySlider.maxValue = mana;
        energySlider.value = mana;
    }

    void Update()
    {
        if (energySlider.value != easeEnergySlider.value)
        {
            // slowly move the health bar to the correct value
            easeEnergySlider.value = Mathf.Lerp(easeEnergySlider.value, energySlider.value, Time.deltaTime * easeSpeed);
        }
    }
}