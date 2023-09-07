using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           Damageable damageable = collision.collider.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.OnHit(damage);
            }
        }
    }
}
