using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;
    
    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private GameObject bulletPrefabOverride;
    /// <summary>
    /// 現在の残弾数
    /// </summary>
    private int currentAmmo;
    /// <summary>
    /// 最後に発射された時間
    /// </summary>
    private float lastFireTime;
    /// <summary>
    /// 
    /// </summary>
    private bool isReloading = false;

    public int GetCurremtAmmo
    {
        get { return currentAmmo; }

    }

    public int GetMaxAmmo
    {
        get { return weaponData.MaxAmmo;  }
    }

    private void OnEnable()
    {

        currentAmmo = weaponData.MaxAmmo;

    }

    public void Fire()
    {
        if (isReloading)
        {
            return;
        }
        if(Time.time - lastFireTime < 1f/ weaponData.FireRate)
        {
            return;
        }
        if(currentAmmo < 0)
        {
            return;
        }
        lastFireTime = Time.time;
        //現在の残弾数をデクリメント(-１)します
        currentAmmo--;

        GameObject bullet = Instantiate(
            bulletPrefabOverride != null ? 
            bulletPrefabOverride : weaponData.BulletPrefab,
            shootPoint.position,shootPoint.rotation
            );
        bullet.GetComponent<Rigidbody>().linearVelocity
            = shootPoint.forward * 30f;

    }

    public void Reload()
    {
        //ローディング中　or 残弾数が最大だった場合
        if (isReloading || currentAmmo == weaponData.MaxAmmo)
        {
            return;
        }
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponData.ReloadTime);
        currentAmmo = weaponData .MaxAmmo;
        isReloading = false;
    }


}
