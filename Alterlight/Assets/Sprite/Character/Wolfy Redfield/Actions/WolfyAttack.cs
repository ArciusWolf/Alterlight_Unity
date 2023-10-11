using UnityEngine;

public class WolfyAttack : MonoBehaviour
{
    public float mana;
    public SwordAttack swordAttack;
    public EnergyBar energyBar;

    private AudioManager audioManager;
    private PlayerSwitch playerSwitch;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerSwitch = GetComponent<PlayerSwitch>();
    }

    private void Start()
    {
        energyBar.setMaxEnergy(mana);
        mana = StatManager.getStatValue("Stamina");
    }

    private void Update()
    {
        if (playerSwitch.WolfyActive())
        {
            IncreaseMana(Time.deltaTime * (playerSwitch.GetComponent<WolfyMovement>().canMove ? 3f : 10f));
            energyBar.setEnergy(mana);
        }

        if (mana > 100)
        {
            mana = 100;
        }
    }

    public void OnFire()
    {
        if (playerSwitch.WolfyActive() && mana > 0)
        {
            GetComponent<Animator>().SetTrigger("isAttack");
            audioManager.PlaySFX(audioManager.Slash);
            DecreaseMana(10f);
            energyBar.setEnergy(mana);
        }
    }

    public void SwordAttack()
    {
        GetComponent<WolfyMovement>().LockMovement();
        if (GetComponent<SpriteRenderer>().flipX)
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

    private void IncreaseMana(float amount)
    {
        mana += amount;
    }

    private void DecreaseMana(float amount)
    {
        mana -= amount;
    }
}