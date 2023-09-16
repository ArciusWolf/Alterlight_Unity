using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CyrusHealth : MonoBehaviour, Damageable
{
    public HealthBar healthBar;
    public float health = 100f;
    public float currentHP;
    public GameObject DamageText;
    public TextMeshProUGUI healthText;
    Animator anim;
    bool _targetable = true;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.setMaxHealth(health);
        anim = GetComponent<Animator>();
    }

    public float Health
    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("isHit");
                RectTransform textDmg = Instantiate(DamageText).GetComponent<RectTransform>();
                textDmg.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindGameObjectWithTag("Hub").GetComponent<Canvas>();
                textDmg.SetParent(canvas.transform);
            }
            health = value;

            if (health <= 0)
            {
                Dead();
                Targetable = false;
            }
        }
        get
        {
            return health;
        }
    }

    public void Dead()
    {
        anim.SetBool("isDead", true);
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;
        }
    }

    public void OnHit(float damage)
    {
        Health -= damage;
        healthText.text = damage.ToString();
        healthBar.setHealth(health);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        healthText.text = damage.ToString();
    }
}