using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTableHandler : MonoBehaviour
{
    [SerializeField] private GameObject scoreLineAsset;

    private void Awake()
    {
        LoadScoresToTable();
        print("load");
    }

    // =========================

    private void LoadScoresToTable()
    {
        // ������� ���������� ����� �������
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);
        // �������� ������� � �������
        List<ScoreRecord> topScores = SaveManager.GetLoadedScores();
        foreach (ScoreRecord scoreRecord in topScores)
        {
            GameObject scoreLineGO = GameObject.Instantiate(scoreLineAsset, this.transform);
            TextMeshProUGUI scoreLine = scoreLineGO.GetComponent<TextMeshProUGUI>();

            TimeSpan scoreTime = TimeSpan.FromSeconds(scoreRecord.time);
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                scoreTime.Minutes, scoreTime.Seconds, scoreTime.Milliseconds);

            scoreLine.text = $"{formattedTime} / {scoreRecord.totalBonuses} / {scoreRecord.totalPoints}";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// ********************

// ������ �������� ������ � �������� ������
[Serializable]
public class ScoreRecord
{
    public float time;
    public int totalBonuses;
    public int totalPoints;
}
