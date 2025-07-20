using UnityEngine;

public class ScoreTrigetHealth : Health
{
    private int score = 10;

    private FPSGameManager fPSGameManager = null;
    protected override void Start()
    {
        base.Start();
        //ゲーム中にランダムに生まれる可能性があるので、
        //Scoreに登場してから取得する
        fPSGameManager = FindAnyObjectByType<FPSGameManager>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (IsDead())
        {
            fPSGameManager.AddScore(score);
            //自分を消す
            Destroy(this.gameObject);
        }
    }



}
