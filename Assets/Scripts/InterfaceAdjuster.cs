using UnityEngine;

public class InterfaceAdjuster : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea; // Для отслеживания изменений безопасной зоны экрана

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Получаем RectTransform панели
        lastSafeArea = Screen.safeArea; // Инициализируем последнюю безопасную зону
        UpdateUIPosition(); // Устанавливаем начальную позицию
    }

    void Update()
    {
        // Если безопасная зона экрана изменилась (например, при смене ориентации)
        if (Screen.safeArea != lastSafeArea)
        {
            lastSafeArea = Screen.safeArea; // Обновляем последнюю безопасную зону
            UpdateUIPosition(); // Обновляем позицию UI-элемента
        }
    }

    // Обновляем позицию UI-элемента в соответствии с безопасной зоной
    private void UpdateUIPosition()
    {
        // Получаем безопасную зону экрана
        Rect safeArea = Screen.safeArea;

        // Преобразуем координаты безопасной зоны экрана в локальные координаты RectTransform
        float safeAreaHeight = safeArea.height;

        // Вычисляем вертикальное смещение, чтобы панель не выходила за пределы безопасной зоны
        rectTransform.anchorMin = new Vector2(0, safeArea.yMin / Screen.height);  // Устанавливаем минимальное значение якоря по вертикали
        rectTransform.anchorMax = new Vector2(1, (safeArea.yMin + safeAreaHeight) / Screen.height); // Максимальное значение якоря по вертикали
    }

    // --------------------

    public void Pause()
    {
        ScenesManager.Pause(this);
    }
}
