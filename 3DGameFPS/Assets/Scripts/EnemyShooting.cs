using UnityEngine;
/// <summary>
///     �G�̎ˌ�����
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;//�e�̃v���n�u

    [SerializeField]
    private float bulletSpeed = 10f;
    /// <summary>
    /// �e�����˂����
    /// </summary>
    [SerializeField]
    private float shootInterval = 2f;
    /// <summary>
    /// �e�����˂����ꏊ
    /// </summary>
    [SerializeField]
    private Transform shootPoint;

    public int maxBullet = 10;
    private int currentBullets;

    private float lastFireTime = 0;
    public float fireRete = 0.5f;
    public float reloadTime = 3f;
    public float reloadTimer = 0.05f;
    /// <summary>
    /// �v���C���[�������Ă���z��
    /// </summary>
    private Transform target;

    private float lastShootTime = -999f;

    private void Start()
    {
        currentBullets = maxBullet;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        if (Time.time - lastShootTime > shootInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
        // �e��0�ɂȂ����烊���[�h���J�n  
        if (currentBullets <= 0)
        {
            reloadTimer += Time.deltaTime;
            // �����[�h����  
            if (reloadTimer >= reloadTime)
            {
                Reload();
            }

        }
    }
    private void Shoot()
    {
        // �e�𐶐����Ĕ���  
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        float bulletSpeed = bullet.GetComponent<bullet>().GetBulletSpeed; // �C���ӏ�: bulletSpeed ���擾  
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTime = 0f;
    }
}
