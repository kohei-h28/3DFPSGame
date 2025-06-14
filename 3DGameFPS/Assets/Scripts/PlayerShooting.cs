using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float bulletSpeed = 20f;
    [SerializeField] private Transform shootPoint;

    public int maxBullet = 10;
    private int currentBullets;

    private float lastFireTime = 0f;
    public float fireRete = 0.5f;

    [SerializeField] private TextMeshProUGUI bulletCountText;

    public float reloadTime = 3f;
    private float reloadTimer = 0f;

    void Start()
    {
        currentBullets = maxBullet;
        UpdateBulletCountUI();
    }

    void Update()
    {
        if (currentBullets <= 0)
        {
            reloadTimer += Time.deltaTime;

            if (reloadTimer >= reloadTime)
            {
                Reload();
            }
        }
    }

    public void OnAttack(InputValue value)
    {
        if (Time.time - lastFireTime < fireRete)
        {
            return;
        }

        if (!value.isPressed)
        {
            return;
        }

        if (currentBullets <= 0)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(shootPoint.forward * bulletSpeed, ForceMode.Impulse);

        lastFireTime = Time.time;
        currentBullets--;
        UpdateBulletCountUI();
    }

    void UpdateBulletCountUI()
    {
        bulletCountText.text = "Remaining Bullet: " + currentBullets;
    }

    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTimer = 0f;
        UpdateBulletCountUI();
    }
}
