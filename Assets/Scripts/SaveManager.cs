using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

public static class SaveManager
{
    private static string pathToSave = Application.persistentDataPath + "/TopScoresData.json";
    public static List<ScoreRecord> topScores = new List<ScoreRecord>();

    // ==============================

    /// <summary>
    /// Добавить счет сессии в топ 10
    /// </summary>
    public static void SaveScore(ScoreRecord scores)
    {
        topScores = GetLoadedScores();

        topScores.Add(scores);
        topScores = topScores.OrderByDescending(record => record.time).ToList();

        if (topScores.Count > 10)
            topScores = topScores.Take(10).ToList();

        var wrapper = new ScoreRecordListWrapper(topScores);
        string jsonScores = JsonUtility.ToJson(wrapper);
        File.WriteAllText(pathToSave, jsonScores);
    }

    // Загрузка сохраненного топ 10
    public static List<ScoreRecord> GetLoadedScores()
    {
        if (File.Exists(pathToSave))
        {
            string jsonScores = File.ReadAllText(pathToSave);
            var wrapper = JsonUtility.FromJson<ScoreRecordListWrapper>(jsonScores);
            topScores = wrapper?.records ?? new List<ScoreRecord>();
            return topScores;
        }
        return new List<ScoreRecord>();
    }

    // *********************

    // Обертка для сохранения списка
    [Serializable]
    public class ScoreRecordListWrapper
    {
        public List<ScoreRecord> records;

        public ScoreRecordListWrapper(List<ScoreRecord> records)
        {
            this.records = records;
        }
    }
}
