using UnityEngine;

public class PlayerHealth : Health
{
    /// <summary>
    /// override:virtual修飾地がついたメソッドを上書きする
    /// </summary>
    /// <param name="damage"></param>
    public override void TakeDamage(int damage)
    {
        //親クラスTakeDamageの処理を行う
        
        base.TakeDamage(damage);

        if (IsDead())
        {
            Debug.Log("プレイヤーの死亡");
        }
    }
}
