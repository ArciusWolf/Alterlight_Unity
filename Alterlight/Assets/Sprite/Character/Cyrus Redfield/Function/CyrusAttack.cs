using UnityEngine;

public class CyrusAttack : MonoBehaviour
{
    public SwordAttack swordAttack;
    PlayerSwitch playerSwitch;

    public void OnFire()
    {
            if (GetComponent<PlayerSwitch>().WolfyActive == false)
            {
                GetComponent<Animator>().SetTrigger("isAttack");
            }
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