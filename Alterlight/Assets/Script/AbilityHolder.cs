using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;

    enum AbilityState
    {
        Ready,
        Cooldown,
        Active
    }
    AbilityState state = AbilityState.Ready;

    public KeyCode key;
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(key))
                {
                    state = AbilityState.Active;
                    ability.Activate(gameObject);
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime = ability.cooldownTime;
                } else
                {
                    state = AbilityState.Ready;
                }
                break;
            case AbilityState.Active:

                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                } else
                {
                    state = AbilityState.Cooldown;
                    cooldownTime = ability.cooldownTime;
                }

                break;
        }
    }
}
