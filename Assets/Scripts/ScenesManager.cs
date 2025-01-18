using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public static class ScenesManager
{
    public static void Pause(MonoBehaviour scriptToStartCoroutine)
    {
        if (SceneManager.GetSceneByName("Pause").isLoaded)
        {
            Resume();
            return;
        }

        SetUIVisibility(false);
        LoadSceneAdditive("Pause", scriptToStartCoroutine);
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        SetUIVisibility(true);
        SceneManager.UnloadSceneAsync("Pause");
        Time.timeScale = 1f;
    }

    // ------------------------

    public static void SetUIVisibility(bool isVisible)
    {
        GameObject UI = GameObject.FindWithTag("UI");
        if (UI != null)
            UI.GetComponent<Canvas>().enabled = isVisible;
    }

    // ------------------------

    // ����� ��� ����������� �������� ����� � ���������� � � ������� �����
    public static void LoadSceneAdditive(string sceneName, MonoBehaviour scriptToStartCoroutine)
    {
        scriptToStartCoroutine.StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // �������� ��� ����������� �������� �����
    private static IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;
    }

    // ------------------------

    public static void SaveScoreAndLoadGameOver()
    { 
        Time.timeScale = 0;

        // ���������� ����� ������
        ScoreRecord scoreRecord = new ScoreRecord();
        GameObject player = GameObject.FindWithTag("Player");
        GameTime gameTime = player.GetComponent<GameTime>();
        BonusTracker bonusTracker = player.GetComponent<BonusTracker>();

        scoreRecord.time = gameTime.remainingTime;
        scoreRecord.totalPoints = bonusTracker.totalBoostPoints;
        scoreRecord.totalBonuses = bonusTracker.totalPickupsCount;

        SaveManager.SaveScore(scoreRecord);

        // �������� ������ ��� 10 ������
        GameObject worldScripts = GameObject.FindWithTag("GameController");
        ScenesManager.SetUIVisibility(false);
        ScenesManager.LoadSceneAdditive("GameOver", bonusTracker);
    }
}
