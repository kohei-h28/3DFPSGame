using UnityEngine;

public class MoveingTarget : MonoBehaviour
{
    /// <summary>
    /// �ړ�����͈�
    /// </summary>
    [SerializeField]
    private float range = 2f;
    /// <summary>
    /// �ړ����鑬�x
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
        //�^�[�Q�b�g�j�󎞂ɒʒm
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
