using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float knockbackForce = 200f;

    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private LayerMask whatDestroyBullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        setStraightVelecity();
        setDeactivate();
    }

    private void setStraightVelecity()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        //if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        if (obj.CompareTag("Enemy"))
        {
            // Particle effect

            // Sound FX

            // Damage enemy
            Damageable damageable = obj.GetComponent<Damageable>();
            if (obj.gameObject.CompareTag("Enemy"))
            {
                DamageableObject canHit = obj.GetComponent<DamageableObject>();
                if (damageable != null)
                {
                    Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

                    Vector2 direction = (obj.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockbackForce;

                    damageable.OnHit(damage, knockback);
                }
                else
                {
                    Debug.Log("Damageable is null");
                }
            }
            // Destroy bullet
            Destroy(gameObject);
        }
    }

    private void setDeactivate()
    {
        //StartCoroutine(Deactivate());
        Destroy(gameObject, 2f);
    }
}
