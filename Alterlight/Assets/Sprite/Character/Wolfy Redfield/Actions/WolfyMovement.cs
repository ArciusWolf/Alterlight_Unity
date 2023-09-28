using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WolfyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D physicsCollider;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
    }

    // FixedUpdate is called at a fixed rate
    void FixedUpdate()
    {
        // play walk up animation
        if (canMove)
        {

        }
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
            if (movementInput.y > 0)
            {
                anim.SetBool("isWalkUp", true);
            }
            else
            {
                anim.SetBool("isWalkUp", false);
            }
            // play walk down animation
            if (movementInput.y < 0)
            {
                anim.SetBool("isWalkDown", true);
            }
            else
            {
                anim.SetBool("isWalkDown", false);
            }
        }
    }


    private bool TryMove(Vector2 direction)
    {
        int Count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
        if (Count == 0)
        {
            Vector2 newPosition = Vector2.Lerp(rb.position, rb.position + direction * speed * Time.fixedDeltaTime, 0.5f);
            rb.MovePosition(newPosition);
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

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    // get movementInput then return
    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
}