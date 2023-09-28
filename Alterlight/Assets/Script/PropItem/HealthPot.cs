using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    public PotCounter potCounter;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // add 1 to healthPotions
            potCounter.healthPotions += 1;
            Destroy(gameObject);
        }
    }
}
