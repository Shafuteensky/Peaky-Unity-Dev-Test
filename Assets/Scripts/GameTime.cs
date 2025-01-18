using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    // Объект таймера на сцене
    [SerializeField] private GameObject timerUI;
    private TextMeshProUGUI timerText;

    public float remainingTime { get; private set; } = 30f;

    private void Awake()
    {
        timerText = timerUI.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (remainingTime == 0)
            return;

        // Конец счета таймера
        if ((remainingTime -= Time.deltaTime) <= 0)
        {
            remainingTime = 0;
        }

        // Подгон float к формату времени
        int minutes = (int)(remainingTime / 60);
        int seconds = (int)(remainingTime % 60); 
        int milliseconds = (int)((remainingTime - Mathf.Floor(remainingTime)) * 100);
        string formattedTime = string.Format(" {0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);

        timerText.text = formattedTime;
    }

    // ====================

    public void AddTime(float timeToAdd)
    {
        remainingTime += timeToAdd;
    }
}
