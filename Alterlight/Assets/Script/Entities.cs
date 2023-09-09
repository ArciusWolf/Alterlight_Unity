using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DamageableObject : MonoBehaviour, Damageable
{
    [SerializeField]
    public float speed = 0.1f;
    [SerializeField]
    public float range;
    [SerializeField]
    public float maxDistance;

    public float health = 20f;
    bool _targetable = true;
    public GameObject DamageText;
    Animator anim;
    Rigidbody2D rigid;
    Collider2D physicsCollider;
    SpriteRenderer spriteRenderer;
    Vector2 wayPoint;
    Transform target;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GetComponent<Transform>();
        setNewDestination();
    }

    void FixedUpdate()
    {
        // set delay of 1 second before next move and delay before set new destination
        if (Time.time > 1f)
        {
            Move();
        }

        anim.SetBool("isMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            setNewDestination();
        }

        // if position.x increse, flip sprite
        if (transform.position.x > wayPoint.x)
        {
            spriteRenderer.flipX = true;
        } else if (transform.position.x < wayPoint.x)
        {
            spriteRenderer.flipX = false;
        }

    }

    void Move()
    {
        // if target is in range, move towards target
        if (Vector2.Distance(transform.position, target.position) < maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        setNewDestination();
    }

    public float Health
    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("isHit");
                // Damage Text when hit
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

    void setNewDestination()
    {
        wayPoint = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
    }

    void moveToNewDestination()
    {

    }
}
