using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfyDash : MonoBehaviour
{
    WolfyMovement WolfyMovement;
    Rigidbody2D rb;

    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;

    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    private void Awake()
    {
        WolfyMovement = GetComponent<WolfyMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                ReadyToDash();
            }
        }
    }

    private void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        DashAIPooling.Instance.GetFromPool();
        lastImageXpos = transform.position.x;

    }

    private void checkDash()
    {
        if (isDashing)
        {
            WolfyMovement.canMove = false;
            rb.velocity 
        }
    }
}
