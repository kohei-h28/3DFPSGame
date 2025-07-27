using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // �@�񕜂���e����int�^�̕ϐ�
    [SerializeField]
    public int addAmmo = 10;

    //�AOnTrggerEnter��Player���������Ă�����
    //Player�̊K�w���ɂ���WeaponSwitcher���擾����
    //weapon.AddTotalAmmo(1�ō�����ϐ�)�ōő�e����ǉ�
    //����AmmoBox�R���|�[�l���g���ǉ�����Ă���GameObject���폜
    private void OnTriggerEnter(Collider other)
    {
        //player�ɓ����������m�F
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                var weaponSwitcher =
                    other.GetComponentInChildren<WeaponSwitcher>();
                weaponSwitcher.GetCurrentWeapon.AddTotalAmmo(addAmmo);
                Destroy(this.gameObject);
            }
        }
    }
}
