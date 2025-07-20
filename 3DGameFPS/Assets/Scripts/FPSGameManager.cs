using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ゲームの状態管理
/// </summary>
public class FPSGameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームオーバーのUI
    /// </summary>
    [SerializeField]
    private GameObject gameOverPanel;

    //①スコア用のint型の変数を用意してください
    [SerializeField]
    private int score = 0;

    //残りターゲット数
    [SerializeField]
    public int tagetCount = 0;

    //ゲームオーバー時に表示されるUI
    [SerializeField]
    private GameObject gameOverUI;

    //スコア表示用UI
    [SerializeField]
    private TextMeshProUGUI scoreText; // 修正: TextAlignment を Text に変更

    private bool isGameOver = false;
    private int targetCount;

    private void Start()
    {
        //ゲームオーバーUIは最初は非表示に
        gameOverUI.SetActive(false);
    }

    //②スコア追加用のメソッドを作成してください。引数の値をスコア用の変数に追加
    public void AddScore(int amonut)
    {
        score += amonut;
        Debug.Log("スコアが追加されました。現在のスコア: " + score);
    }

    public int GetScore()
    {
        return score;
    }
    //ターゲット破壊時に呼び出す
    public void OnTargetDestroyed()
    {
        targetCount--;

        if (targetCount <= 0 && !isGameOver)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("ゲームオーバー");
        gameOverUI.SetActive(true);
        scoreText.text = "Score: " + score; // 修正: TextAlignment ではなく Text を使用
                                          // カーソルのモードを変更し、カーソル自体を表示します
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // 一時停止
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        // BuildScene内のIndexでシーンをロードする
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
