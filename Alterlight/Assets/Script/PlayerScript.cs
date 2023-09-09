using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour, Damageable
{
    public float speed = 1f;
    public HealthBar healthBar;
    public float health = 100f;
    public float currentHP;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;
    public GameObject DamageText;
    public TextMeshProUGUI healthText;
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D physicsCollider;
    public SwordAttack swordAttack;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;
    bool isAlive = true;
    public bool _targetable = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(health);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();

    }
    // Play hit animation when hit and reduce health
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
    // If health is 0, play dead animation and remove player
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

    public void Dead()
    {
        anim.SetBool("isDead", true);
        isAlive = false;
    }
    // Toggle player targetable
    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;

            rb.simulated = value;
            physicsCollider.enabled = value;
        }
    }
    // Remove player function
    public void RemovePlayer()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0f));
                }
                if (!success)
                {
                    success = TryMove(new Vector2(0f, movementInput.y));
                }
                // Player Animation when moving
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }
            // Flip Character when x < 0 or x > 0
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
    private bool TryMove(Vector2 direction)
    {
        int Count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
        if (Count == 0)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
    void OnFire()
    {
        anim.SetTrigger("isAttack");
    }
    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }
    public void EndAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }
    public void OnHit(float damage)
    {
        Health -= damage;
        healthText.text = damage.ToString();
        healthBar.setHealth(health);
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        healthText.text = damage.ToString();
        rb.AddForce(knockback);
    }
}
