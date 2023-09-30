using UnityEngine;

public class WolfyAttack : MonoBehaviour
{
    public float mana = 100f;

    public SwordAttack swordAttack;
    PlayerSwitch playerSwitch;
    AudioManager audioManager;
    public EnergyBar energyBar;

    private void Start()
    {
        energyBar.setMaxEnergy(mana);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        // Slowly regenerate energy over time and update value
        // generate mana faster when standing still, slower when moving
        if (GetComponent<PlayerSwitch>().WolfyActive)
        {
            if (GetComponent<WolfyMovement>().canMove)
            {
                mana += Time.deltaTime * 3f;
            }
            else
            {
                mana += Time.deltaTime * 10f;
            }
            energyBar.setEnergy(mana);
        }
        // Do not allow mana to go over 100
        if (mana > 100)
        {
            mana = 100;
        }
    }

    public void OnFire()
    {
        if (GetComponent<PlayerSwitch>().WolfyActive)
        {
            GetComponent<Animator>().SetTrigger("isAttack");
            //play attack sound
            audioManager.PlaySFX(audioManager.Slash);
            // Decrease mana when attacking
            mana -= 10f;
            energyBar.setEnergy(mana);
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