using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Weaponのデータ
    /// </summary>
    [SerializeField]
    private WeaponData[] weapons;
    /// <summary>
    ///  Weaponの親オブジェクトのtransfoam(位置)
    /// </summary>
    [SerializeField]
    private Transform weaponHolder;

    /// <summary>
    /// Game上で生成される武器のゲームオブジェクト
    /// </summary>
    private GameObject[] weaponInstances;

    private int currentIndex = 0;

    [SerializeField]
    public int maxAmmo = 30;//現在の最大弾数
    [SerializeField]
    public int totalAmmo = 30;//持っている全弾数
    void Start()
    {
        //武器ので他の数だけ、GameObjectの配列を作成して代入
        weaponInstances = new GameObject[weapons.Length];


        for(int i = 0; i < weaponInstances.Length; i++)
        {
            //
            GameObject weaponObj = Instantiate(weapons[i].
                WeaponPrefab, weaponHolder);
            // 武器を非表示
            weaponObj.SetActive(false);

            weaponInstances[i] = weaponObj;

        }
        weaponInstances[currentIndex].SetActive(true);
    }

    public void Switch(int direcion) 
    {
        //%は余剰を計算
        //例えば武器が２種類で
        weaponInstances[currentIndex].SetActive(false);
        currentIndex = (currentIndex + direcion + weaponInstances.Length)
            % weaponInstances.Length;
        weaponInstances[currentIndex].SetActive(true);
    }
    //最大弾数を追加する処理
    public void AddTotalAmmo(int amount)
    {
        totalAmmo += amount;
        Debug.Log("総弾数が増えました" + totalAmmo);
    }

    internal void AddTotalAmmo(object ammoAmount)
    {
        throw new NotImplementedException();
    }

    public Weapon GetCurrentWeapon
    {
        get { 
            return weaponInstances[currentIndex].GetComponent<Weapon>();
        }
    }
}
