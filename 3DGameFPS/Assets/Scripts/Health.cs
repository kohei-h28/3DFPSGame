using UnityEngine;
/// <summary>
/// 体力の管理
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// 体力の最大値
    /// </summary>
    [SerializeField]
    private int maxHealthPoint = 5;
    /// <summary>
    /// 現在の体力値
    /// </summary>
    private int currentHealthPoint;
    /// <summary>
    /// protected:自分自身継承した子クラスから失せす出来る
    /// virtual:子クラスメソッドの内容を上書きできる
    /// </summary>
    protected virtual void Start()
    {
        currentHealthPoint = maxHealthPoint;
    }
    
    ///だめーぎ処理
    ///＜damage＞現在体力値ラひかれているダメージ
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
