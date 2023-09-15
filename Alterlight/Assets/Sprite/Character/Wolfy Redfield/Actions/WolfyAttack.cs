using UnityEngine;

public class WolfyAttack : MonoBehaviour
{
    public SwordAttack swordAttack;

    void OnFire()
    {
        GetComponent<Animator>().SetTrigger("isAttack");
    }

    public void SwordAttack()
    {
        GetComponent<WolfyMovement>().LockMovement();

        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndAttack()
    {
        GetComponent<WolfyMovement>().UnlockMovement();
        swordAttack.StopAttack();
    }
}