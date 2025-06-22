using UnityEngine;
/// <summary>
/// �̗͂̊Ǘ�
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// �̗͂̍ő�l
    /// </summary>
    [SerializeField]
    private int maxHealthPoint = 5;
    /// <summary>
    /// ���݂̗̑͒l
    /// </summary>
    private int currentHealthPoint;
    /// <summary>
    /// protected:�������g�p�������q�N���X���玸�����o����
    /// virtual:�q�N���X���\�b�h�̓��e���㏑���ł���
    /// </summary>
    protected virtual void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }
    
    ///���߁[������
    ///��damage�����ݑ̗͒l���Ђ���Ă���_���[�W
    public virtual void TakeDamage(int damage)
    {
        currentHealthPoint -= damage;
        Debug.Log(currentHealthPoint);
    }

    public bool IsDead()
    {
        return currentHealthPoint <= 0;
    }
}
