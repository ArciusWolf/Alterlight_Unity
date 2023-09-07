using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    public Detector detector;
    Animator anim;

    DamageableObject DamageableObject;

    void Start()
    {
        anim = GetComponent<Animator>();
        DamageableObject = GetComponent<DamageableObject>();
    }
    private void FixedUpdate()
    {
        if(DamageableObject.Targetable && detector.detectedObjects.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, detector.detectedObjects[0].transform.position, 0.1f * Time.deltaTime);
            anim.SetBool("isMoving", true);
        } else if (detector.detectedObjects.Count == 0)
        {
            anim.SetBool("isMoving", false);
        }
    }

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
