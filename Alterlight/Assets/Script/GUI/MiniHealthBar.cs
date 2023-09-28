using BlazeAIDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public HealthBar healthBar; // Reference to the HealthBar component
    CyrusHealth CyrusHealth;
    WolfyHealth WolfyHealth;
    
    public GameObject MiniCyrus;
    public GameObject MiniWolfy;

    // Start is called before the first frame update
    void Start()
    {
        CyrusHealth = GameObject.Find("Cyrus").GetComponent<CyrusHealth>();
        WolfyHealth = GameObject.Find("Wolfy").GetComponent<WolfyHealth>();
    }

    // check if MiniCyrus is active Or MiniWolfy is active then set the healthSlider value to the current health
    void Update()
    {
        if (MiniCyrus.activeSelf)
        {
            healthSlider.value = CyrusHealth.health;
        }
        else if (MiniWolfy.activeSelf)
        {
            healthSlider.value = WolfyHealth.health;
        }
    }


}
