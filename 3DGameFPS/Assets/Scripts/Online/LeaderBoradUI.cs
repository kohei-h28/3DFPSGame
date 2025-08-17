using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Build.Player;

public class LeaderBoradUI : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;

    [SerializeField]
    private TextMeshProUGUI tebleText;
    
    void Start()
    {
        resultPanel.SetActive(false);
        if(NetworkScoreboad.Instance != null)
        {
            NetworkScoreboad.Instance.Scores.OnListChanged += OnScoresChange;
            Refesh();
        }
    }

    private void OnDestroy()
    {
        if (NetworkScoreboad.Instance != null)
        {
            NetworkScoreboad.Instance.Scores.OnListChanged -= OnScoresChange;

        }
    }
    /// <summary>
    /// リザルト画面を表示します
    /// </summary>
    public void ShowResult()
    {
        Refesh();
        resultPanel.SetActive(true);
    }

    private void OnScoresChange(Unity.Netcode.NetworkListEvent<ScoreEntry> e) => Refesh();

    /// <summary>
    /// リーダーボードの中身を切り替えます
    /// </summary>
    private void Refesh()
    {
        var scores = NetworkScoreboad.Instance.Scores;
        if(scores == null)
        {
            return;
        }
        var scoreList = new List<ScoreEntry>(scores.Count);
        for (int i = 0; i < scores.Count; i++)
        {
            scoreList.Add(scores[i]);
        }

        scoreList.Sort((a,b)=>b.Score.CompareTo(a.Score));

        var scoreText = new System.Text.StringBuilder();
        for (int i = 0; i < scoreList.Count; i++)
        {
            scoreText.AppendLine($"{i + 1}.Player ID {scoreList[i].ClientId}:{scoreList[i].Score}");

        }
        if(scoreText != null)
        {
            tebleText.text = scoreText.ToString();
        }
    }
    
}
