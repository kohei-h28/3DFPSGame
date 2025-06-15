using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾のプレハブ  

    [SerializeField] private Transform shootPoint; // 発射位置  

    public int maxBullet = 10; // 最大弾数  
    private int currentBullets; // 現在の弾数  

    private float lastFireTime = 0f;
    public float fireRete = 0.5f;

    [SerializeField] private TextMeshProUGUI bulletCountText;

    public float reloadTime = 3f; // リロード時間  
    private float reloadTimer = 0f;

    private Camera maincamera;
    void Start()
    {
        currentBullets = maxBullet; // 初期弾数を設定  
        UpdateBulletCountUI(); // 弾数をUIに反映  
        maincamera = Camera.main;
    }

    void Update()
    {
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

    // 攻撃ボタンが押されたとき  
    public void OnAttack(InputValue value)
    {
        // プレイヤーが連射しないように制限  
        if (Time.time - lastFireTime < fireRete)
        {
            return;
        }
        // ボタンが押されていない場合は処理しない  
        if (!value.isPressed)
        {
            return;
        }

        if (currentBullets <= 0)
        {
            return;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            Debug.Log($"hit:{hit.collider.name}");
        }

        // 弾を生成して発射  
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        float bulletSpeed = bullet.GetComponent<bullet>().GetBulletSpeed; // 修正箇所: bulletSpeed を取得  
        bulletRigidbody.AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);

        lastFireTime = Time.time;
        currentBullets--;
        UpdateBulletCountUI();
    }

    // 弾数をUIに反映  
    void UpdateBulletCountUI()
    {
        bulletCountText.text = "Remaining Bullet: " + currentBullets;
    }

    // 弾数を最大値に戻し、タイマーをリセット  
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTimer = 0f;
        UpdateBulletCountUI();
    }
}
