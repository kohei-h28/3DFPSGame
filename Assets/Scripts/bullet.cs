using UnityEngine;

public class bullet : MonoBehaviour
{
    // �Փˎ��ɒe�������鏈��
    void OnCollisionEnter(Collision collision)
    {
        // �����ɏՓ˂�����e������
        Destroy(gameObject);
    }
}
