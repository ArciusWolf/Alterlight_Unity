using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.1f;
    [SerializeField]
    public float range;
    [SerializeField]
    public float maxDistance;

    bool canMove = true;

    Vector2 wayPoint;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("setNewDestination", Random.Range(1f, 3f)); // Set a delay before setting the first destination
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            anim.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);

            // Flip sprite when moving left or right
            if (transform.position.x > wayPoint.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (transform.position.x < wayPoint.x)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void setNewDestination()
    {
        // Set a delay
        Invoke("setNewDestination", Random.Range(1f, 3f));
        wayPoint = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
    }

    public Vector2 GetWayPoint() // Method to get the current waypoint
    {
        return wayPoint;
    }

    public void SetWayPoint(Vector2 newWayPoint) // Method to set a new waypoint
    {
        wayPoint = newWayPoint;
    }
}
