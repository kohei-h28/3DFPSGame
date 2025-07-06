
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputRelay : MonoBehaviour
{
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    public void OnAttack(InputValue value)
    {

        if (value.isPressed)
        {
            weaponSwitcher.GetCurrentWeapon.Fire();

        }
    }
    public void OnReload(InputValue value)
    { // inputActionÇ…ê›íËÇ≥ÇÍÇΩReload(RÇ´Å[)Ç™âüÇ≥ÇÍÇΩÇÁ
        weaponSwitcher.GetCurrentWeapon.Reload();
        Debug.Log("Reload");
    }

    public void OnWeaponSwitch(InputValue value)
    {
        var inputValue = value.Get<Vector2>();
        if (inputValue.y > 0)
        {
            weaponSwitcher.Switch(1);
        }
        else if (inputValue.y < 0)
        {
            weaponSwitcher.Switch(-1);
        }
    }
    public void OnAiming(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("aim");
        }
        else
        {
            Debug.Log("Aimout");
        }
    }
}
