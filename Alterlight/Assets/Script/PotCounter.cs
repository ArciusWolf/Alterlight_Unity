using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotCounter : MonoBehaviour
{
    public int healthPotions = 3; // Number of health potions
    public int manaPotions = 3; // Number of mana potions

    public TextMeshProUGUI healthPotionText; // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI manaPotionText; // Reference to the TextMeshProUGUI component

    // Update is called once per frame
    void Update()
    {
        // put healthPotion value textmeshpro toString
        healthPotionText.text = healthPotions.ToString();
        // put manaPotion value textmeshpro toString
        manaPotionText.text = manaPotions.ToString();
    }
}
