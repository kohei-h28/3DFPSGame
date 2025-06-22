using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffectPrefab;

    [SerializeField]
    private float lifeTime = 5f;//弾丸が消えるまでの時間

    [SerializeField]
    private AudioClip hitSound;//弾丸に付与する効果音

    [SerializeField] private float bulletSpeed = 1f;//弾丸の速度

    /// <summary>
    /// 外部から弾の速度を取得するプロパティ
    /// </summary>
   public float GetBulletSpeed
    {
        get { return bulletSpeed; }
    }
    // 衝突時に弾が消える処理
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突位置と表面の法線ベクトルを取得
        ContactPoint contact = collision.contacts[0];

        if (hitEffectPrefab != null)
        {
            // ヒットエフェクトを衝突位置に、表面の方向に合わせて生成
            Instantiate(hitEffectPrefab,
                        contact.point,
                        Quaternion.LookRotation(contact.normal));
        }
        //
        AudioSource.PlayClipAtPoint(hitSound, contact.point);

        if (collision.gameObject.TryGetComponent<Health>(out Health health)) 
        {
            health.TakeDamage(1);
        }
        // 弾を消す
        Destroy(this.gameObject, 0.1f);
    }
}
