using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WolfyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D physicsCollider;
    Vector2 movementInput;
    Rigidbody2D rb;
    bool canMove;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
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
}
