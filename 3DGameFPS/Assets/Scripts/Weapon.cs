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
    /// Œ»İ‚Ìc’e”
    /// </summary>
    private int currentAmmo;
    /// <summary>
    /// ÅŒã‚É”­Ë‚³‚ê‚½ŠÔ
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
    /// <summary>
    /// ‘Š’e”
    /// </summary>
    public int totalAmmo;

    public int GetTotalAmmo
    {
        get { return totalAmmo; }
    }
    /// <summary>
    /// gameObiject‚ªActive‚É‚È‚Á‚½‚É”­‰Î
    /// </summary>
    private void OnEnable()
    {

        currentAmmo = weaponData.MaxAmmo;
        //‘Š’e”‚ğ‰Šú‰»
        totalAmmo = weaponData.MaxTotalAmmo;
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
        //Œ»İ‚Ìc’e”‚ğƒfƒNƒŠƒƒ“ƒg(-‚P)‚µ‚Ü‚·
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
        //ƒ[ƒfƒBƒ“ƒO’†@or c’e”‚ªÅ‘å‚¾‚Á‚½ê‡
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

        var needed = weaponData.MaxAmmo - currentAmmo;
        var taken = Mathf.Min(needed, totalAmmo);
        currentAmmo += taken;
        totalAmmo -= taken;
        isReloading = false;
    }
    /// <summary>
    /// ŠO•”‚©‚ç’e”‚ğ•â[‚·‚é
    /// </summary>
    /// <param name="ammo">•â[‚·‚é’e”</param>
    public void AddTotalAmmo(int ammo)
    {
        // Mathf.Min‚Í
        totalAmmo = Mathf.Min(totalAmmo + ammo, weaponData.MaxTotalAmmo);
    }

}
