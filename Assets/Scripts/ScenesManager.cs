using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScene;

    // ==========================

    public void Pause()
    {
        if (SceneManager.GetSceneByName("Pause").isLoaded)
        {
            Resume();
            return;
        }

        LoadSceneAdditive("Pause");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        SceneManager.UnloadSceneAsync("Pause");
        Time.timeScale = 1f;
    }

    // ------------------------

    // ����� ��� ����������� �������� ����� � ���������� � � ������� �����
    private void LoadSceneAdditive(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // �������� ��� ����������� �������� �����
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
