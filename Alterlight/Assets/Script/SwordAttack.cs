using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    Vector2 RightAttackOffset;
    public float knockbackForce = 500f;
    public float damage = 10f;
    Rigidbody2D rb;

    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogError("Sword Collider is null");
        }
        RightAttackOffset = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = RightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(RightAttackOffset.x * -1, RightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        Damageable damageable = obj.GetComponent<Damageable>();
        if (obj.gameObject.CompareTag("Enemy"))
        {
            DamageableObject canHit = obj.GetComponent<DamageableObject>();
            if (damageable != null)
            {
                Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

                Vector2 direction = (obj.transform.position - parentPosition).normalized;
                Vector2 knockback = direction * knockbackForce;

                damageable.OnHit(damage, knockback);
            }
            else
            {
                Debug.Log("Damageable is null");
            }
        }
    }
}
