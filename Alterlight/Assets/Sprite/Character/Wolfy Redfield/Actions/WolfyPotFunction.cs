using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfyPotFunction : MonoBehaviour
{
    WolfyHealth wolfyHealth;
    public PotCounter potCounter; // Reference to the PotCounter component

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component - taking damage
    public DamageNumber healText;
    // Reference to the DamageNumber component - alert
    public DamageNumber alertText;
    // Start is called before the first frame update
    void Start()
    {
        wolfyHealth = GetComponent<WolfyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (potCounter.healthPotions > 0 && wolfyHealth.health < 100)
            {
                wolfyHealth.health += 20;
                potCounter.healthPotions--;
                wolfyHealth.healthBar.setHealth(wolfyHealth.health);
                wolfyHealth.healText.SetFollowedTarget(wolfyHealth.textPosition);
                DamageNumber damageNumber = wolfyHealth.healText.Spawn(wolfyHealth.GetWolfyPosition(), 20);
                damageNumber.SetFollowedTarget(wolfyHealth.textPosition);
            }
            else if (potCounter.healthPotions == 0)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "No health potion left!", textPosition);
            }
            else if (wolfyHealth.health == 100)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "Wolfy health is full!", textPosition);
                damageNumber.SetFollowedTarget(textPosition);
            }
        }

        // if overhealing, set health to 100
        if (wolfyHealth.health > 100)
        {
            wolfyHealth.health = 100;
        }
    }
}
