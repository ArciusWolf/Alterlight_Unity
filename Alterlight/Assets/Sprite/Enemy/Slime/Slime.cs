using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;

    public Detector detector;
    Animator anim;

    HostileFunction function;
    Hostile hostile;
    public float damageCooldown = 1f; // The cooldown period in seconds

    private float lastDamageTime;
    void Start()
    {
        anim = GetComponent<Animator>();
        function = GetComponent<HostileFunction>();
        hostile = GetComponent<Hostile>(); // Initialize the hostile object
        Invoke("setNewDestination", Random.Range(1f, 3f)); // Set a delay before setting the first destination
    }

    private void FixedUpdate()
    {
        if (function.Targetable)
        {
            if (detector.detectedObjects.Count > 0 && detector.detectedObjects[0] != null)
            {
                // Update the waypoint to the player's position
                hostile.SetWayPoint(detector.detectedObjects[0].transform.position);
            }

            // Move towards the waypoint
            transform.position = Vector2.MoveTowards(transform.position, hostile.GetWayPoint(), 0.1f * Time.deltaTime);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Damageable damageable = collision.collider.GetComponent<Damageable>();
    //        if (damageable != null)
    //        {
    //            damageable.OnHit(damage);
    //        }
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GetComponent<Collider2D>().IsTouching(collision))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null && Time.time >= lastDamageTime + damageCooldown)
            {
                damageable.OnHit(damage);
                lastDamageTime = Time.time;
            }
        }
    }





    void setNewDestination()
    {
        // Set a delay
        Invoke("setNewDestination", Random.Range(1f, 3f));
        hostile.setNewDestination(); // Set a new destination when there are no detected objects
    }
}
