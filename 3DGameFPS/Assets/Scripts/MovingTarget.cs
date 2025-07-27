using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    /// <summary>
    /// ˆÚ“®‚·‚é”ÍˆÍ
    /// </summary>
    [SerializeField]
    private float range = 2f;
    /// <summary>
    /// ˆÚ“®‚·‚é‘¬“x
    /// </summary>
    [SerializeField]
    private float speed = 2f;

    private Vector3 startPos;

    private FPSGameManager gameManager;

    public int Length { get; internal set; }

    [SerializeField]public int hp = 1;

    void Start()
    {
        gameManager = FindAnyObjectByType<FPSGameManager>();
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var offset = Mathf.Sin(Time.time * speed) * range;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }

    private void OnDestroy()
    {
        
        if(gameManager != null)
        {
            gameManager.OnTargetDestroyed();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Õ“Ë‚µ‚½‘Šè: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
