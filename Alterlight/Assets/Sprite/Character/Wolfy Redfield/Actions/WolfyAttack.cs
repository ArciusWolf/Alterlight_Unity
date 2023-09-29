using UnityEngine;

public class WolfyAttack : MonoBehaviour
{
    public SwordAttack swordAttack;
    PlayerSwitch playerSwitch;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void OnFire()
    {
        if (GetComponent<PlayerSwitch>().WolfyActive)
        {
            GetComponent<Animator>().SetTrigger("isAttack");
            //play attack sound
            audioManager.PlaySFX(audioManager.Slash);
        }
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