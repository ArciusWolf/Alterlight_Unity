using UnityEngine;

public class CyrusFollow : MonoBehaviour
{
    public GameObject Wolfy;
    public GameObject enemy;
    public float attackRange = 3f;
    public float moveSpeed = 5f;

    private bool isFollowing;

    void Start()
    {
        isFollowing = false;
    }

    void Update()
    {
        if (enemy != null)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= attackRange)
            {
                // Trigger attack behavior
                Attack();
            }
            else
            {
                // Move towards the enemy
                MoveTowardsEnemy();
            }
        }
        else if (isFollowing)
        {
            // Move towards Wolfy
            MoveTowardsWolfy();
        }
    }

    void Attack()
    {
        // Implement your attack logic or animation here
    }

    void MoveTowardsEnemy()
    {
        Vector3 direction = (enemy.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void MoveTowardsWolfy()
    {
        Vector3 direction = (Wolfy.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}