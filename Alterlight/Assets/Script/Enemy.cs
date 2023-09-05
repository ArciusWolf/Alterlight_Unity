using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public float Health

    {
        set
        {
            if (value < health)
            {
                anim.SetTrigger("isHit");
                health = value;
            }
            if (health <= 0)
            {
                Dead();
            }
        }
        get
        {
            return health;
        }
    }
    public void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    public float health = 20f;



    public void Dead()
    {
        anim.SetTrigger("isDead");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject, 3);
    }

/*    public void OnHit(float damage)
    {
          Health -= health;
    }*/
}
