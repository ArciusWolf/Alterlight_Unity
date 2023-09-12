using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WolfyAttack : MonoBehaviour
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

    void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
