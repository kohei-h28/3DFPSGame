using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    
    /// <summary>
    /// �c�e��
    /// </summary>
    public int maxBullet = 10; // �ő�i��
    private int currentBullets;// ���݂̒e��
    
    // �A�ː����p�̃^�C�}�[
    private float lastFireTime = 0f;
    // ���ˊԊu����
    public float fireRete = 0.5f;

    // UI�ɕ\�������邽�߂�TextMeshPro
    [SerializeField]
    private TextMechProUGUI bulletCountText;

    //���������[�h�Ɋւ���ݒ�
    public float reloadTime = 3f;//�����[�h����(�b)
    private float reloadTimer = 0f;//�����[�h�p�̃^�C�}�[

    void Start()
    {
        //
        currentBullets = maxBullet;
        UpdateBulletCountUI();//
    }
    public void OnAttack(InputValue value)
    {
        // ���݂̎��Ԃ��Ō�ɒe�𔭎˂������Ԃ��甭�ˊԊu�𒴂��Ă���ꍇ
        if (lastFireTime.time - lastFireTime < fireRete)
        {
            return;// ���ˊԊu���o�߂��Ă��Ȃ���Ή������Ȃ�
        }
        
        if (!value.isPressed)
        {
            return;
        }

        // �e�����c���Ă��Ȃ��ꍇ�͉������Ȃ�
        if (currentBullets < 0)
        {
            return;
        }

        // �e�𔭎�
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

        // �Ō�ɔ��˂������Ԃ��X�V
        lastFireTime = Time.time;

        // �c�e�������炷
        currentBullets--;// UI��\��
    }   

    // �c�e����UI���X�V���郁�\�b�h    
    void UpdateBulletCountUI()
    {
         bulletCountText.text = 
            "Remaining Bullet:" + currentBullets;
    }

    // �e���������[�h���郁�\�b�h(�K�v�ɉ����Ēǉ�)
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTimer = 0f; // �����[�h�^�C�}�[�����Z�b�g
        UpdateBulletCountUI();
    }
}