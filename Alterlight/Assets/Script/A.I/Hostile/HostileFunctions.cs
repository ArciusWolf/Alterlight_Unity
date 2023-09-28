using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DamageNumbersPro;

public class HostileFunction : MonoBehaviour, Damageable
{
    public EnemyHPBar healthBar; // Reference to the HealthBar component
    public float health = 20f;
    bool _targetable = true;

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component
    public DamageNumber dmgText;

    public List<Collider2D> detectedObjects = new List<Collider2D>();
    Animator anim;
    Rigidbody2D rigid;
    Collider2D physicsCollider;

    public void Start()
    {
        healthBar.setMaxHealth(health);
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        textPosition = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedObjects.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedObjects.Remove(collision);
        }
    }

    public float Health
    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("isHit");
            }
            health = value;
            if (health <= 0)
            {
                Dead();
                Targetable = false;
            }
        }
        get
        {
            return health;
        }
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
            rigid.simulated = value;
            physicsCollider.enabled = value;
        }
    }

    public void Dead()
    {
        anim.SetTrigger("isDead");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
        
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        // Set the damage text to follow the object
        dmgText.SetFollowedTarget(textPosition);
        // Spawn a damage number at the object's position
        DamageNumber damageNumber = dmgText.Spawn(GetPosition(), damage);
        healthBar.setHealth(health);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        dmgText.SetFollowedTarget(textPosition);
        DamageNumber damageNumber = dmgText.Spawn(GetPosition(), damage);
        damageNumber.SetFollowedTarget(textPosition);
        rigid.AddForce(knockback);
        healthBar.setHealth(health);
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}