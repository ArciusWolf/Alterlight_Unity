using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WolfyMovement : MonoBehaviour
{
    float speed;
    [SerializeField] private float collisionOffset = 0.1f;
    [SerializeField] private ContactFilter2D movementFilter;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Collider2D physicsCollider;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public bool canMove = true;

    private void Start()
    {
        speed = StatManager.getStatValue("Speed");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
            HandleAnimations();
        }
    }

    private void HandleMovement()
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
        }
    }

    private void HandleAnimations()
    {
        anim.SetBool("isMove", movementInput != Vector2.zero);
        spriteRenderer.flipX = movementInput.x < 0;
        //anim.SetBool("isWalkUp", movementInput.y > 0);
        //anim.SetBool("isWalkDown", movementInput.y < 0);
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
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

    private void OnMove(InputValue movementValue)
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

    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
}