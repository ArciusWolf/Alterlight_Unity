using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DamageableObject : MonoBehaviour, Damageable
{
    Animator anim;
    Rigidbody2D rigid;
    Collider2D physicsCollider;
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
    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }
    public float health = 20f;

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

    public bool _targetable = true;

    public void Dead()
    {
        anim.SetTrigger("isDead");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject, 3);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rigid.AddForce(knockback, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.collider.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.OnHit(10f);
        }
    }
}
