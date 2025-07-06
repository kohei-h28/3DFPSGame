using UnityEditor.Experimental.GraphView;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    public Weapon GetCurrentWeapon
    {
        get { 
            return weaponInstances[currentIndex].GetComponent<Weapon>();
        }
    }
}
