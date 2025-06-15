using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;//�e�̃v���n�u
    private float bulletSpeed = 20f;//�e�̃X�s�[�h
    [SerializeField] private Transform shootPoint;//���ˈʒu(��U��Player�̑O��)

    public int maxBullet = 10;//�ő呍�e��
    private int currentBullets;//�c�e��

    private float lastFireTime = 0f;
    public float fireRete = 0.5f;

    [SerializeField] private TextMeshProUGUI bulletCountText;

    public float reloadTime = 3f;//�����[�h��������
    private float reloadTimer = 0f;

    void Start()
    {
        currentBullets = maxBullet;//��������̎c�e��
        UpdateBulletCountUI();//�e�̐�����ʂɕ\��
    }

    void Update()
    {
        //���܂��O�ɂȂ����烊���[�h���n�߂�
        if (currentBullets <= 0)
        {
            reloadTimer += Time.deltaTime;
            //�R�b�������烊���[�h
            if (reloadTimer >= reloadTime)
            {
                Reload();
            }
        }
    }
    //�U���{�^������������
    public void OnAttack(InputValue value)
    {
        //�v���C���[���}�E�X���������Ƃ�
        if (Time.time - lastFireTime < fireRete)
        {
            return;
        }
        //�A�ˏo���Ȃ��悤��0.5�b�łĂȂ�
        if (!value.isPressed)
        {
            return;
        }

        if (currentBullets <= 0)
        {
            return;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0, 0.5f));

        if(Physics.Raycast(ray, out RaycastHit hit, 20f))
        {

           Debug.Log($"hit:{hit.collider.name}");
        }

        //�e���Ȃ��Ȃ�����łĂȂ�
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
        
        lastFireTime = Time.time;
        currentBullets--;
        UpdateBulletCountUI();
    }
    //�e������ʂɕ\��
    void UpdateBulletCountUI()
    {
        bulletCountText.text = "Remaining Bullet: " + currentBullets;
    }
    //�e��10���ɖ߂��ă^�C�}�[�����Z�b�g
    private void Reload()
    {
        currentBullets = maxBullet;
        reloadTimer = 0f;
        UpdateBulletCountUI();
    }
}
