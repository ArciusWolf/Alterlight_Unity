using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public CyrusAttack cyrusAttack;
    public WolfyAttack wolfyAttack;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GetComponent<PlayerSwitch>().WolfyActive)
            {
                wolfyAttack.OnFire();
            }
            else
            {
                cyrusAttack.OnFire();
            }
        }
    }
}