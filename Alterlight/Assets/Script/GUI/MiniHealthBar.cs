using BlazeAIDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider energySlider;
    public HealthBar healthBar; // Reference to the HealthBar component
    public EnergyBar energyBar; // Reference to the EnergyBar component
    CyrusHealth CyrusHealth;
    CyrusAttack CyrusAttack;
    WolfyHealth WolfyHealth;
    WolfyAttack WolfyAttack;
    
    public GameObject MiniCyrus;
    public GameObject MiniWolfy;

    // Start is called before the first frame update
    void Start()
    {
        CyrusHealth = GameObject.Find("Cyrus").GetComponent<CyrusHealth>();
        WolfyHealth = GameObject.Find("Wolfy").GetComponent<WolfyHealth>();
        CyrusAttack = GameObject.Find("Cyrus").GetComponent<CyrusAttack>();
        WolfyAttack = GameObject.Find("Wolfy").GetComponent<WolfyAttack>();
    }

    // check if MiniCyrus is active Or MiniWolfy is active then set the healthSlider value to the current health
    void Update()
    {
        if (MiniCyrus.activeSelf)
        {
            healthSlider.value = CyrusHealth.health;
            energySlider.value = CyrusAttack.mana;
        }
        else if (MiniWolfy.activeSelf)
        {
            healthSlider.value = WolfyHealth.wolfyHP;
            energySlider.value = WolfyAttack.mana;
        }
    }


}
