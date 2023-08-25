using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 1f;
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
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
