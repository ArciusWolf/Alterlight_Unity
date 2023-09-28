using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dash : Ability
{
    public float dashSpeed;

    public override void Activate(GameObject parent)
    {
        WolfyMovement movement = parent.GetComponent<WolfyMovement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        rb.velocity = movement.GetMovementInput().normalized * dashSpeed;
    }
}
