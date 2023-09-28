using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;

public class CyrusHealth : MonoBehaviour, Damageable
{
    public HealthBar healthBar; // Reference to the HealthBar component
    public float health = 100f; // Initial health value
    public float currentHP; // Current health value

    Animator anim;

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component - taking damage
    public DamageNumber dmgText;
    // Reference to the DamageNumber component - healing
    public DamageNumber healText;
    // Reference to the DamageNumber component - alert
    public DamageNumber alertText;

    bool _targetable = true; // Flag to determine if the object is targetable

    // Start is called before the first frame update
    void Start()
    {
        // Set the maximum health value for the health bar
        healthBar.setMaxHealth(health);
        anim = GetComponent<Animator>();
        textPosition = GetComponent<Transform>();
    }

    // limit health value to 100 only
    public float HealthLimit(float health)
    {
        if (health > 100)
        {
            health = 100;
        }
        return health;
    }

    public float Health
    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("isHit"); // Trigger the "isHit" animation if the health value is decreased
            }
            health = value; // Update the health value

            if (health <= 0)
            {
                Dead(); // Call the Dead method if the health reaches or goes below 0
                Targetable = false; // Set the targetable flag to false
            }
        }
        get
        {
            return health; // Return the current health value
        }
    }

    public void Dead()
    {
        anim.SetBool("isDead", true); // Set the "isDead" parameter of the animator to true
    }

    public bool Targetable
    {
        get { return _targetable; } // Return the targetable flag
        set
        {
            _targetable = value; // Update the targetable flag
        }
    }

    public void OnHit(float damage)
    {
        // Reduce the health by the damage amount
        Health -= damage;
        // Set the damage text to follow the object
        dmgText.SetFollowedTarget(textPosition);
        // Spawn a damage number at the object's position
        DamageNumber damageNumber = dmgText.Spawn(GetCyrusPosition(), damage);
        // Update the health bar with the new health value
        healthBar.setHealth(health);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        dmgText.SetFollowedTarget(textPosition);
        DamageNumber damageNumber = dmgText.Spawn(GetCyrusPosition(), damage);
        damageNumber.SetFollowedTarget(textPosition);
    }

    // Get the Cyrus gameObject position as Vector3
    public Vector3 GetCyrusPosition()
    {
        return gameObject.transform.position;
    }
}