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
    /// ���݂̎c�e��
    /// </summary>
    private int currentAmmo;
    /// <summary>
    /// �Ō�ɔ��˂��ꂽ����
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
        //���݂̎c�e�����f�N�������g(-�P)���܂�
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
        //���[�f�B���O���@or �c�e�����ő傾�����ꍇ
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
