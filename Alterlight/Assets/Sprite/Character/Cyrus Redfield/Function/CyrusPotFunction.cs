using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyrusPotFunction : MonoBehaviour
{
    CyrusHealth cyrusHealth;
    public PotCounter potCounter; // Reference to the PotCounter component

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component - taking damage
    public DamageNumber healText;
    // Reference to the DamageNumber component - alert
    public DamageNumber alertText;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        cyrusHealth = GetComponent<CyrusHealth>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (potCounter.healthPotions > 0 && cyrusHealth.health < 100)
            {
                cyrusHealth.health += 20;
                potCounter.healthPotions--;
                cyrusHealth.healthBar.setHealth(cyrusHealth.health);
                cyrusHealth.healText.SetFollowedTarget(cyrusHealth.textPosition);
                DamageNumber damageNumber = cyrusHealth.healText.Spawn(cyrusHealth.GetCyrusPosition(), 20);
                damageNumber.SetFollowedTarget(cyrusHealth.textPosition);
                audioManager.PlaySFX(audioManager.Healing);
            }
            else if (potCounter.healthPotions == 0)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(cyrusHealth.GetCyrusPosition(), "No health potion left!", textPosition);
            }
            else if (cyrusHealth.health == 100)
            {
                alertText.SetFollowedTarget(textPosition);
                DamageNumber damageNumber = alertText.Spawn(cyrusHealth.GetCyrusPosition(), "Your health is full!", textPosition);
                damageNumber.SetFollowedTarget(textPosition);
            }
        }

        // if overhealing, set health to 100
        if (cyrusHealth.health > 100)
        {
            cyrusHealth.health = 100;
        }
    }
}
