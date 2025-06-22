using UnityEngine;
using TMPro;
using System.Collections;
/// <summary>
/// �e�ۂ̊Ǘ�
/// </summary>
public class Shooting : MonoBehaviour
{
    [SerializeField]
    protected GameObject BulletPrefab;

    [SerializeField] protected Transform shootPoint;
    protected int bulletsFired = 0;

    protected int maxBullets = 10;

    protected float cooldownDuration = 10f;

    protected bool isCooldown = false;
    /// <summary>
    /// �ˌ�����
    /// </summary>
    /// <param name="direction">�ł���</param>
    /// <param name="remainingText">�c�e��\������K�v������Ύw��:null�ő��</param>
    protected virtual void Shoot(Vector3 direction,TextMeshProUGUI remainingText = null)
    {
        if(bulletsFired >= maxBullets)
        {
            StartCoroutine(StartCooldown(remainingText));
            return;
        }
        GameObject bullet = Instantiate(BulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        float speed = bullet.GetComponent<Bullet>().GetBulletSpeed; // �C���ӏ�: bulletSpeed ���擾  
        bulletRb.AddForce(direction * speed, ForceMode.Impulse);
        bulletsFired++;
    }

    protected IEnumerator StartCooldown(TextMeshProUGUI remainingText = null)
    {
        isCooldown = true;
        Debug.Log("�e�؂�I�N�[���_�E����...");
        yield return new WaitForSeconds(cooldownDuration);
        bulletsFired = 0;
        isCooldown = false;
        Debug.Log("�đ��U����");
        if (remainingText != null) ;
        {
            remainingText.text = $"{maxBullets}/{maxBullets}";
        }

    }
}
