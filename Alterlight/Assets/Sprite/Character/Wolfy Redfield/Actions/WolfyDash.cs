using UnityEngine;

public class WolfyDash : MonoBehaviour
{
    WolfyMovement WolfyMovement;
    Rigidbody2D rb;
    PlayerSwitch playerSwitch;

    public bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;

    public float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    private void Awake()
    {
        playerSwitch = GetComponent<PlayerSwitch>();
        WolfyMovement = GetComponent<WolfyMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerSwitch.WolfyActive())
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                ReadyToDash();
            }
        }
        checkDash();
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
            if (dashTimeLeft > 0)
            {
                WolfyMovement.canMove = false;
                rb.velocity = new Vector2(dashSpeed * WolfyMovement.movementInput.x, dashSpeed * WolfyMovement.movementInput.y);
                // calculate the dash time left
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    DashAIPooling.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                WolfyMovement.canMove = true;
            }
        }
    }
}
