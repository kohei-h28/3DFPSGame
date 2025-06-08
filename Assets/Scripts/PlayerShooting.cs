using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerShooting : MonoBehaviour
{

    /// <summary>
    /// 弾丸のプレハブ
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// 弾丸の速度
    /// </summary>
    private float bulletSpeed = 20f;

    /// <summary>
    /// 弾丸の発射位置
    /// </summary>
    [SerializeField]
    private Transform shootPoint;
    
    /// <summary>
    /// 残弾数
    /// </summary>
    public int maxBullet = 10; // 最大段数
    private int currentBullets;// 現在の弾数
    
    // 連射制限用のタイマー
    private float lastFireTime = 0f;
    // 発射間隔時間
    public float fireRete = 0.5f;

    // UIに表示させるためのTextMeshPro
    [SerializeField]
    private TextMechProUGUI bulletCountText;

    //自動リロードに関する設定
    public float reloadTime = 3f;//リロード時間(秒)
    private float reloadTimer = 0f;//リロード用のタイマー

    void Start()
    {
        //
        currentBullets = maxBullet;
        UpdateBulletCountUI();//
    }
    public void OnAttack(InputValue value)
    {
        // 現在の時間が最後に弾を発射した時間から発射間隔を超えている場合
        if (lastFireTime.time - lastFireTime < fireRete)
        {
            return;// 発射間隔が経過していなければ何もしない
        }
        
        if (!value.isPressed)
        {
            return;
        }

        // 弾数が残っていない場合は何もしない
        if (currentBullets < 0)
        {
            return;
        }

        // 弾を発射
        GameObject bullet = Instantiate(
            bulletPrefab,//生成するゲームオブジェクト
            shootPoint.position,//生成する位置
            shootPoint.rotation//生成する角度
            );

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // 力を加える
        bulletRigidbody.AddForce
            (shootPoint.forward * bulletSpeed,
            ForceMode.Impulse);

        // 最後に発射した時間を更新
        lastFireTime = Time.time;

        // 残弾数を減らす
        currentBullets--;// UIを表示
    }   

    // 残弾数のUIを更新するメソッド    
    void UpdateBulletCountUI()
    {
         bulletCountText.text = 
            "Remaining Bullet:" + currentBullets;
    }

    // 弾数をリロードするメソッド(必要に応じて追加)
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTimer = 0f; // リロードタイマーをリセット
        UpdateBulletCountUI();
    }
}