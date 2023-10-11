using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DamageNumbersPro;
using Unity.VisualScripting;

public class WolfyHealth : MonoBehaviour, Damageable
{
    public HealthBar healthBar; // Reference to the HealthBar component
    //public float health = 100f; // Initial health value
    //public float currentHP; // Current health value

    public float wolfyHP;

    Animator anim;

    // Reference to the Transform component for text position
    public Transform textPosition;
    // Reference to the DamageNumber component - taking damage
    public DamageNumber dmgText;
    // Reference to the DamageNumber component - healing
    public DamageNumber healText;
    // Reference to the DamageNumber component - mana
    public DamageNumber manaText;
    // Reference to the DamageNumber component - alert
    public DamageNumber alertText;

    // Reference to the AudioManager component
    AudioManager audioManager;

    bool _targetable = true; // Flag to determine if the object is targetable

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        anim = GetComponent<Animator>();
        textPosition = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        wolfyHP = StatManager.getStatValue("Health");
        // Set the maximum health value for the health bar
        healthBar.setMaxHealth(StatManager.getStatValue("Health"));
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
            if (value < wolfyHP)
            {
                anim.SetTrigger("isHit"); // Trigger the "isHit" animation if the health value is decreased
                audioManager.PlaySFX(audioManager.PlayerHit);
            }
            wolfyHP = value; // Update the health value

            if (wolfyHP <= 0)
            {
                Dead(); // Call the Dead method if the health reaches or goes below 0
                Targetable = false; // Set the targetable flag to false
            }
        }
        get
        {
            return wolfyHP; // Return the current health value
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
        DamageNumber damageNumber = dmgText.Spawn(GetWolfyPosition(), damage);
        // Update the health bar with the new health value
        healthBar.setHealth(wolfyHP);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        dmgText.SetFollowedTarget(textPosition);
        DamageNumber damageNumber = dmgText.Spawn(GetWolfyPosition(), damage);
        damageNumber.SetFollowedTarget(textPosition);
    }

    // Get the Wolfy gameObject position as Vector3
    public Vector3 GetWolfyPosition()
    {
        return gameObject.transform.position;
    }
}