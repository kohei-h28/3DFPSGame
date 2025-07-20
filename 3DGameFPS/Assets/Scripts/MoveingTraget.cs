using UnityEngine;

public class MoveingTarget : MonoBehaviour
{
    /// <summary>
    /// 移動する範囲
    /// </summary>
    [SerializeField]
    private float range = 2f;
    /// <summary>
    /// 移動する速度
    /// </summary>
    [SerializeField]
    private float speed = 2f;

    private Vector3 startPos;

    private FPSGameManager gameManager;
    void Start()
    {
        startPos = this.transform.position;
        gameManager = FindAnyObjectByType<FPSGameManager>();
        gameManager.tagetCount++;
    }
    private void OnDestroy()
    {
        //ターゲット破壊時に通知
        if(gameManager != null)
        {
            gameManager.OnTargetDestroyed();
        }
    }

    void Update()
    {
        var offset = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}
