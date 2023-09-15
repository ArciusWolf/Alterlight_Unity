using UnityEngine;

public class CyrusAttack : MonoBehaviour
{
    public SwordAttack swordAttack;

    void OnFire()
    {
        GetComponent<Animator>().SetTrigger("isAttack");
    }

    public void SwordAttack()
    {
        GetComponent<CyrusMovement>().LockMovement();

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
        GetComponent<CyrusMovement>().UnlockMovement();
        swordAttack.StopAttack();
    }
}