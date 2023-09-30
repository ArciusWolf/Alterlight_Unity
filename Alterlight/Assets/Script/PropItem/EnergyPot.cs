using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPot : MonoBehaviour
{
    public PotCounter potCounter;
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // add 1 to healthPotions
            potCounter.EnergyPotions += 1;
            audioManager.PlaySFX(audioManager.HealthPickUp);
            // destroy the health potion
            Destroy(gameObject);
        }
    }
}
