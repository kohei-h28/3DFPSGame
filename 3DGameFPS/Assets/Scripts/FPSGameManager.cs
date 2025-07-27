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
        MovingTarget[] targets = FindObjectsOfType<MovingTarget>(); // �C��: FindObjectsOfType���g�p���Ĕz����擾
        targetCount = targets.Length;
        Debug.Log("�����^�[�Q�b�g��: " + targetCount);
        foreach (var t in targets)
        {
            Debug.Log("�^�[�Q�b�g����: " + t.name);
        }

        GameOverPanel.SetActive(false);
    }



    // �X�R�A�����Z
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("�X�R�A���Z: " + amount + "�i���݃X�R�A: " + score + "�j");
    }

    // �^�[�Q�b�g��1�̔j�󂳂�邲�ƂɌĂ΂��
    public void OnTargetDestroyed()
    {
        if (isGameOver) return; //��d�Ăяo����h�~

        targetCount--;

        if (targetCount <= 0)
        {
            
            GameOver();
        }
    }

    // �Q�[���I�[�o�[����
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
        // C�L�[�ŃN���C�A���g���[�h�Őڑ�����
        if (Keyboard.current.cKey.isPressed)
        {
            networkManager.StartClient();
        }
    }
}
