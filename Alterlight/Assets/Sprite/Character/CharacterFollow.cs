using System.Collections;
using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    public PlayerSwitch playerSwitch;
    public float followSpeed = 0.1f;
    public float rotationSpeed = 0.1f; // Speed of rotation
    public float randomRadius = 3.0f; // Radius for random movement
    public float attackRange = 1.0f; // Range at which the character will attack the enemy
    private Vector3 velocity = Vector3.zero;
    private Animator animator; // Animator component
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        GameObject target; // The character to follow

        if (playerSwitch.WolfyActive)
        {
            target = playerSwitch.WolfyController.gameObject;
        }
        else
        {
            target = playerSwitch.CyrusController.gameObject;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > 1)
        {
            // Get a random point around the target
            Vector3 randomPoint = target.transform.position + Random.insideUnitSphere * randomRadius;

            // Add some variability to the follow speed and rotation speed
            float variableFollowSpeed = followSpeed + Random.Range(-0.05f, 0.05f);
            float variableRotationSpeed = rotationSpeed + Random.Range(-0.05f, 0.05f);

            // Move towards the random point
            transform.position = Vector3.SmoothDamp(transform.position, randomPoint, ref velocity, variableFollowSpeed);

            // Rotate towards the target over time
            Quaternion toRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, variableRotationSpeed * Time.deltaTime);

            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("isMove", true); // Set isMove to true when moving
            }
        }
        else if (isMoving)
        {
            isMoving = false;
            StartCoroutine(StopMoving());
        }

        // If the enemy is within attack range, attack the enemy
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            AttackEnemy(target);
        }
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(1); // Wait for 1 second before stopping the animation
        animator.SetBool("isMove", false); // Set isMove to false when not moving
    }

    void AttackEnemy(GameObject enemy)
    {
        // Implement your attack logic here
    }
}
