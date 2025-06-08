using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour
{

    /// <summary>
    /// �e�ۂ̃v���n�u
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// �e�ۂ̑��x
    /// </summary>
    private float bulletSpeed = 20f;
    /// <summary>
    /// �e�ۂ̔��ˈʒu
    /// </summary>
    [SerializeField]
    private Transform shootPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnAttack(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefab,//��������Q�[���I�u�W�F�N�g
            shootPoint.position,//��������ʒu
            shootPoint.rotation//��������p�x
            );
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        // �͂�������
        bulletRigidbody.AddForce
            (shootPoint.forward * bulletSpeed,
            ForceMode.Impulse);

    }
}