using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �Q�[���̏�ԊǗ�
/// </summary>
public class FPSGameManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���I�[�o�[��UI
    /// </summary>
    [SerializeField]
    private GameObject gameOverPanel;

    //�@�X�R�A�p��int�^�̕ϐ���p�ӂ��Ă�������
    [SerializeField]
    private int score = 0;

    //�c��^�[�Q�b�g��
    [SerializeField]
    public int tagetCount = 0;

    //�Q�[���I�[�o�[���ɕ\�������UI
    [SerializeField]
    private GameObject gameOverUI;

    //�X�R�A�\���pUI
    [SerializeField]
    private TextMeshProUGUI scoreText; // �C��: TextAlignment �� Text �ɕύX

    private bool isGameOver = false;
    private int targetCount;

    private void Start()
    {
        //�Q�[���I�[�o�[UI�͍ŏ��͔�\����
        gameOverUI.SetActive(false);
    }

    //�A�X�R�A�ǉ��p�̃��\�b�h���쐬���Ă��������B�����̒l���X�R�A�p�̕ϐ��ɒǉ�
    public void AddScore(int amonut)
    {
        score += amonut;
        Debug.Log("�X�R�A���ǉ�����܂����B���݂̃X�R�A: " + score);
    }

    public int GetScore()
    {
        return score;
    }
    //�^�[�Q�b�g�j�󎞂ɌĂяo��
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
        Debug.Log("�Q�[���I�[�o�[");
        gameOverUI.SetActive(true);
        scoreText.text = "Score: " + score; // �C��: TextAlignment �ł͂Ȃ� Text ���g�p
                                          // �J�[�\���̃��[�h��ύX���A�J�[�\�����̂�\�����܂�
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // �ꎞ��~
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        // BuildScene����Index�ŃV�[�������[�h����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
