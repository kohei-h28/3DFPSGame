using UnityEngine;
/// <summary>
///     敵の射撃処理
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;//弾のプレハブ

    [SerializeField]
    private float bulletSpeed = 10f;
    /// <summary>
    /// 弾が発射される
    /// </summary>
    [SerializeField]
    private float shootInterval = 2f;
    /// <summary>
    /// 弾が発射される場所
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
    /// プレイヤーが入ってくる想定
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
        // 弾が0になったらリロードを開始  
        if (currentBullets <= 0)
        {
            reloadTimer += Time.deltaTime;
            // リロード完了  
            if (reloadTimer >= reloadTime)
            {
                Reload();
            }

        }
    }
    private void Shoot()
    {
        // 弾を生成して発射  
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        float bulletSpeed = bullet.GetComponent<bullet>().GetBulletSpeed; // 修正箇所: bulletSpeed を取得  
        bulletRigidbody.AddForce(this.transform.forward * bulletSpeed, ForceMode.Impulse);

    }
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTime = 0f;
    }
}
