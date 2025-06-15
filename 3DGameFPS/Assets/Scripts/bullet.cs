using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffectPrefab;

    [SerializeField]
    private float lifeTime = 5f;

    [SerializeField]
    private AudioClip hitSound;
    
    private void Start()
    {
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
        if(hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, contact.point);
        }
       

        // 弾を消す
        Destroy(gameObject, 0.1f);
    }
}
