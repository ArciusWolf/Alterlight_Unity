using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator anim;
    public float Health
    {
        set
        {
             health = value;
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
    }
    public float health = 20f;

    public void Dead()
    {
        anim.SetTrigger("isDead");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }


}
