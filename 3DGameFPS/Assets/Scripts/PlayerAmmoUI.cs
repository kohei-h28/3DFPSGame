using UnityEngine;
using TMPro;

public class PlayerAmmoUI : MonoBehaviour
{
    //‡@TextMeshProUGUI‚Ì•Ï”‚ğì¬
    [SerializeField]
    private TextMeshProUGUI ammoText;
    //‡AWeaponSwitcher‚Ì•Ï”‚ğì¬
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;


    void Update()
    {
        //TextMeshProUGUI‚Ì•Ï”‚ÌText‚É‘Î‚µ‚Ä
        //WeaponSwitcher‚Ì•Ï”‚ª‚Á‚Ä‚¢‚écurrentAmmo‚ÆmaxAmmo
        //‚ğ•\¦‚·‚é
        ammoText.text =
            $"{weaponSwitcher.GetCurrentWeapon.GetCurremtAmmo}" +
            $"/{weaponSwitcher.GetCurrentWeapon.GetMaxAmmo} ";

    }
}
