using UnityEngine;
using TMPro;
using System.Collections;
/// <summary>
/// 弾丸の管理
/// </summary>
public class Shooting : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletPrefab;

    [SerializeField] protected Transform shootPoint;
    protected int bulletsFired = 0;

    protected int maxBullets = 10;

    protected float cooldownDuration = 10f;

    protected bool isCooldown = false;
    /// <summary>
    /// 射撃処理
    /// </summary>
    /// <param name="direction">打つ方向</param>
    /// <param name="remainingText">残弾を表示する必要があれば指定:nullで代入</param>
    protected virtual void Shoot(Vector3 direction,TextMeshProUGUI remainingText = null)
    {
        if(bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown(remainingText));
            return;
        }
        GameObject bullet = Instantiate(BulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        float speed = bullet.GetComponent<Bullet>().GetBulletSpeed; // 修正箇所: bulletSpeed を取得  
        bulletRb.AddForce(direction * speed, ForceMode.Impulse);
        bulletsFired++;
    }

    protected IEnumerator StartCooldown(TextMeshProUGUI remainingText = null)
    {
        isCooldown = true;
        Debug.Log("弾切れ！クールダウン中...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("再装填完了");
        if (remainingText != null) ;
        {
            remainingText.text = $"{maxBullets}/{maxBullets}";
        }

    }
}
