using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffectPrefab;

    [SerializeField]
    private float lifeTime = 5f;

    [SerializeField]
    private AudioClip hitSound;
    
    private void Start()
    {
    }

    // �Փˎ��ɒe�������鏈��
    private void OnCollisionEnter(Collision collision)
    {
        // �Փˈʒu�ƕ\�ʂ̖@���x�N�g�����擾
        ContactPoint contact = collision.contacts[0];

        if (hitEffectPrefab != null)
        {
            // �q�b�g�G�t�F�N�g���Փˈʒu�ɁA�\�ʂ̕����ɍ��킹�Đ���
            Instantiate(hitEffectPrefab,
                        contact.point,
                        Quaternion.LookRotation(contact.normal));
        }
        if(hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, contact.point);
        }
       

        // �e������
        Destroy(gameObject, 0.1f);
    }
}
