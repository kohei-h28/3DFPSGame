using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class FPSGameManager : MonoBehaviour
{
    public int score = 0;
    public int targetCount = 0;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private NetworkManager networkManager;
    private bool isGameOver = false;

    [System.Obsolete]
    void Start()
    {
        MovingTarget[] targets = FindObjectsOfType<MovingTarget>(); // 修正: FindObjectsOfTypeを使用して配列を取得
        targetCount = targets.Length;
        Debug.Log("初期ターゲット数: " + targetCount);
        foreach (var t in targets)
        {
            Debug.Log("ターゲット発見: " + t.name);
        }

        GameOverPanel.SetActive(false);
    }



    // スコアを加算
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("スコア加算: " + amount + "（現在スコア: " + score + "）");
    }

    // ターゲットが1体破壊されるごとに呼ばれる
    public void OnTargetDestroyed()
    {
        if (isGameOver) return; //二重呼び出しを防止

        targetCount--;

        if (targetCount <= 0)
        {
            
            GameOver();
        }
    }

    // ゲームオーバー処理
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (GameOverPanel != null)
        GameOverPanel.SetActive(true);

        if (scoreText != null)
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void Update()
    {
        // Cキーでクライアントモードで接続する
        if (Keyboard.current.cKey.isPressed)
        {
            networkManager.StartClient();
        }
    }
}
