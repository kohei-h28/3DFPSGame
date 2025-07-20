using UnityEngine;

public class ScoreTrigetHealth : Health
{
    private int score = 10;

    private FPSGameManager fPSGameManager = null;
    protected override void Start()
    {
        base.Start();
        //�Q�[�����Ƀ����_���ɐ��܂��\��������̂ŁA
        //Score�ɓo�ꂵ�Ă���擾����
        fPSGameManager = FindAnyObjectByType<FPSGameManager>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            fPSGameManager.AddScore(score);
            //����������
            Destroy(this.gameObject);
        }
    }



}
