using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    /// <summary>
    /// Weapon�̃f�[�^
    /// </summary>
    [SerializeField]
    private WeaponData[] weapons;
    /// <summary>
    ///  Weapon�̐e�I�u�W�F�N�g��transfoam(�ʒu)
    /// </summary>
    [SerializeField]
    private Transform weaponHolder;

    /// <summary>
    /// Game��Ő�������镐��̃Q�[���I�u�W�F�N�g
    /// </summary>
    private GameObject[] weaponInstances;

    private int currentIndex = 0;

    [SerializeField]
    public int maxAmmo = 30;//���݂̍ő�e��
    [SerializeField]
    public int totalAmmo = 30;//�����Ă���S�e��
    void Start()
    {
        //����̂ő��̐������AGameObject�̔z����쐬���đ��
        weaponInstances = new GameObject[weapons.Length];


        for(int i = 0; i < weaponInstances.Length; i++)
        {
            //
            GameObject weaponObj = Instantiate(weapons[i].
                WeaponPrefab, weaponHolder);
            // ������\��
            weaponObj.SetActive(false);

            weaponInstances[i] = weaponObj;

        }
        weaponInstances[currentIndex].SetActive(true);
    }

    public void Switch(int direcion) 
    {
        //%�͗]����v�Z
        //�Ⴆ�Ε��킪�Q��ނ�
        weaponInstances[currentIndex].SetActive(false);
        currentIndex = (currentIndex + direcion + weaponInstances.Length)
            % weaponInstances.Length;
        weaponInstances[currentIndex].SetActive(true);
    }
    //�ő�e����ǉ����鏈��
    public void AddTotalAmmo(int amount)
    {
        totalAmmo += amount;
        Debug.Log("���e���������܂���" + totalAmmo);
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
