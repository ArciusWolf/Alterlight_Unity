using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfyPotFunction : MonoBehaviour
{
    WolfyHealth wolfyHealth;
    WolfyAttack wolfyAttack;
    public PotCounter potCounter; // Reference to the PotCounter component

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component - taking damage
    public DamageNumber healText;
    // Reference to the DamageNumber component - alert
    public DamageNumber alertText;
    // Reference to the DamageNumber component - mana
    public DamageNumber manaText;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        wolfyHealth = GetComponent<WolfyHealth>();
        wolfyAttack = GetComponent<WolfyAttack>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (potCounter.healthPotions > 0 && wolfyHealth.wolfyHP < 100)
            {
                wolfyHealth.wolfyHP += 20;
                potCounter.healthPotions--;
                wolfyHealth.healthBar.setHealth(wolfyHealth.wolfyHP);
                wolfyHealth.healText.SetFollowedTarget(wolfyHealth.textPosition);
                DamageNumber damageNumber = wolfyHealth.healText.Spawn(wolfyHealth.GetWolfyPosition(), 20);
                damageNumber.SetFollowedTarget(wolfyHealth.textPosition);
                audioManager.PlaySFX(audioManager.Healing);
            }
            else if (potCounter.healthPotions == 0)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "No health potion left!", textPosition);
            }
            else if (wolfyHealth.wolfyHP == 100)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "Wolfy health is full!", textPosition);
                damageNumber.SetFollowedTarget(textPosition);
            }
        }

        // press B to use mana potion
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (potCounter.EnergyPotions > 0 && wolfyAttack.mana < 100)
            {
                wolfyAttack.mana += 20;
                potCounter.EnergyPotions--;
                wolfyAttack.energyBar.setEnergy(wolfyAttack.mana);
                wolfyHealth.manaText.SetFollowedTarget(wolfyHealth.textPosition);
                DamageNumber damageNumber = wolfyHealth.manaText.Spawn(wolfyHealth.GetWolfyPosition(), 20);
                damageNumber.SetFollowedTarget(wolfyHealth.textPosition);
                audioManager.PlaySFX(audioManager.Healing);
            }
            else if (potCounter.EnergyPotions == 0)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "No mana potion left!", textPosition);
            }
            else if (wolfyAttack.mana == 100)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(wolfyHealth.GetWolfyPosition(), "Wolfy mana is full!", textPosition);
                damageNumber.SetFollowedTarget(textPosition);
            }
        }

        // if overhealing, set health to 100
        if (wolfyHealth.wolfyHP > 100)
        {
            wolfyHealth.wolfyHP = 100;
        }
    }
}
