using UnityEngine;
using UnityEngine.InputSystem;

public class AimShoot : MonoBehaviour
{
    [SerializeField] private GameObject gunPivot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;

    private GameObject bulletInst;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    PlayerSwitch playerSwitch;

    private void Awake()
    {
        playerSwitch = GetComponent<PlayerSwitch>();
    }

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        if (!playerSwitch.WolfyActive()){
            worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction = (worldPosition - (Vector2)gunPivot.transform.position).normalized;
            gunPivot.transform.right = direction;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Vector3 localScale = new Vector3(0.59f, 0.59f, 0.59f);
            if (angle > 90 || angle < -90)
            {
                localScale.y = -0.59f;
            }
            else
            {
                localScale.y = 0.59f;
            }
            gunPivot.transform.localScale = localScale;
        }

    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !playerSwitch.WolfyActive()) {
            bulletInst = Instantiate(bulletPrefab, bulletSpawnPoint.position, gunPivot.transform.rotation);
        }
    }
}
