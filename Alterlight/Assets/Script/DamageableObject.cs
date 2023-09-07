using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class DamageableObject : MonoBehaviour, Damageable
{
    public GameObject DamageText;
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
                RectTransform textDmg = Instantiate(DamageText).GetComponent<RectTransform>();
                textDmg.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindGameObjectWithTag("Hub").GetComponent<Canvas>();
                textDmg.SetParent(canvas.transform);
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

    bool _targetable = true;

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
        rigid.AddForce(knockback);
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Damageable damageable = collision.collider.GetComponent<Damageable>();

    //        if (damageable != null)
    //        {
    //            damageable.OnHit(10f);
    //        }
    //    }
    //}
}
