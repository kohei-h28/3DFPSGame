using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // ‡@‰ñ•œ‚·‚é’e”‚ÌintŒ^‚Ì•Ï”
    [SerializeField]
    public int ammoAmount = 10;

    //‡AOnTrggerEnter‚ÅPlayer‚ª“–‚½‚Á‚Ä‚«‚½‚ç
    //Player‚ÌŠK‘w‰º‚É‚ ‚éWeaponSwitcher‚ğæ“¾‚µ‚Ä
    //weapon.AddTotalAmmo(1‚Åì‚Á‚½•Ï”)‚ÅÅ‘å’e”‚ğ’Ç‰Á
    //‚±‚ÌAmmoBoxƒRƒ“ƒ|[ƒlƒ“ƒg‚ª’Ç‰Á‚³‚ê‚Ä‚¢‚éGameObject‚ğíœ
    private void OnTriggerEnter(Collider other)
    {
        //player‚É“–‚½‚Á‚½‚©Šm”F
        if (other.CompareTag("Player"))
        {
            //Player‚ÌŠK‘w‰º‚É‚ ‚éWeaponSwitcher‚ğæ“¾
            var weaponSwitcher =
                other.GetComponentInChildren<WeaponSwitcher>();
            if (weaponSwitcher != null)
            {
                //Å‘å’e”‚ğ’Ç‰Á
                weaponSwitcher.AddTotalAmmo(ammoAmount);
                //‚±‚ÌAmmoBox‚ğíœ
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning
                    ("weaponSwither‚ªplayer‚ÌŠK‘w‰º‚ÉŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½");
            }
        }
    }
}
