using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Tilemaps;

public class HostileFunction : MonoBehaviour, Damageable
{
    public float health = 20f;
    bool _targetable = true;
    public GameObject DamageText;
    public List<Collider2D> detectedObjects = new List<Collider2D>();
    Animator anim;
    Rigidbody2D rigid;
    Collider2D physicsCollider;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
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
}