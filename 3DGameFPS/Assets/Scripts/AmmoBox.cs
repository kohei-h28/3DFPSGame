using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    // �@�񕜂���e����int�^�̕ϐ�
    [SerializeField]
    public int ammoAmount = 10;

    //�AOnTrggerEnter��Player���������Ă�����
    //Player�̊K�w���ɂ���WeaponSwitcher���擾����
    //weapon.AddTotalAmmo(1�ō�����ϐ�)�ōő�e����ǉ�
    //����AmmoBox�R���|�[�l���g���ǉ�����Ă���GameObject���폜
    private void OnTriggerEnter(Collider other)
    {
        //player�ɓ����������m�F
        if (other.CompareTag("Player"))
        {
            //Player�̊K�w���ɂ���WeaponSwitcher���擾
            var weaponSwitcher =
                other.GetComponentInChildren<WeaponSwitcher>();
            if (weaponSwitcher != null)
            {
                //�ő�e����ǉ�
                weaponSwitcher.AddTotalAmmo(ammoAmount);
                //����AmmoBox���폜
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning
                    ("weaponSwither��player�̊K�w���Ɍ�����܂���ł���");
            }
        }
    }
}
