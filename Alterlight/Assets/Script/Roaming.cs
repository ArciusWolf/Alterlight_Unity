using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Roaming : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float range;
    [SerializeField]
    public float maxDistance;
    Animator anim;

    Vector2 wayPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
        setNewDestination();
    }

    private void Update()
    {
        anim.SetBool("isMoving", true);
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            setNewDestination();
        }   
    }

    void setNewDestination()
    {
        wayPoint = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
    }
}