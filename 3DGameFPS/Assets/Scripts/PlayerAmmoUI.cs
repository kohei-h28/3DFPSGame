using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    //�@TextMeshProUGUI�̕ϐ����쐬
    [SerializeField]
    private TextMeshProUGUI ammoText;
    //�AWeaponSwitcher�̕ϐ����쐬
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;
    //TotalMaxAmmo�̕ϐ����쐬
    [SerializeField]
    private TextMeshProUGUI totalMaxAmmoText;

    public void SetWeaponSwitcher(WeaponSwitcher weaponSwitcher)
    {
        this.weaponSwitcher = weaponSwitcher;
    }

    void Update()
    {
        //TextMeshProUGUI�̕ϐ���Text�ɑ΂���
        //WeaponSwitcher�̕ϐ��������Ă���currentAmmo��maxAmmo
        //��\������
        ammoText.text =
            $"{weaponSwitcher.GetCurrentWeapon.GetCurremtAmmo}" +
            $"/{weaponSwitcher.GetCurrentWeapon.GetMaxAmmo} ";
        totalMaxAmmoText.text =
            $"{weaponSwitcher.GetCurrentWeapon.GetTotalAmmo}";
    }
}
