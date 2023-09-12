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

    bool canMove = true;
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
        // If there is player in list, cancel the waypoint and move towards the player

        if (canMove && health > 0)
        {
            anim.SetBool("isMoving", true);
            // Move towards the waypoint
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        // Flip sprite when moving left or right
        if (transform.position.x > wayPoint.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x < wayPoint.x)
        {
            spriteRenderer.flipX = false;
        }
        // if touch a collider, lock movement and position for 0.5 second then unlock after 1 second
        if (rigid.IsTouchingLayers(LayerMask.GetMask("Default")))
        {
            lockMovement();
            anim.SetBool("isIdle", true);
            Invoke("unlockMovement", 2f);
        }
    }

    void lockMovement()
    {
        canMove = false;
    }
    void unlockMovement()
    {
        canMove = true;
        anim.SetBool("isIdle", false);
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
        // Set a delay
        Invoke("setNewDestination", Random.Range(1f, 3f));
        wayPoint = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
    }

    void moveToNewDestination()
    {

    }
}
