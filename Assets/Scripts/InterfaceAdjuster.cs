using UnityEngine;

public class InterfaceAdjuster : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea; // ��� ������������ ��������� ���������� ���� ������

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // �������� RectTransform ������
        lastSafeArea = Screen.safeArea; // �������������� ��������� ���������� ����
        UpdateUIPosition(); // ������������� ��������� �������
    }

    void Update()
    {
        // ���� ���������� ���� ������ ���������� (��������, ��� ����� ����������)
        if (Screen.safeArea != lastSafeArea)
        {
            lastSafeArea = Screen.safeArea; // ��������� ��������� ���������� ����
            UpdateUIPosition(); // ��������� ������� UI-��������
        }
    }

    // ��������� ������� UI-�������� � ������������ � ���������� �����
    private void UpdateUIPosition()
    {
        // �������� ���������� ���� ������
        Rect safeArea = Screen.safeArea;

        // ����������� ���������� ���������� ���� ������ � ��������� ���������� RectTransform
        float safeAreaHeight = safeArea.height;

        // ��������� ������������ ��������, ����� ������ �� �������� �� ������� ���������� ����
        rectTransform.anchorMin = new Vector2(0, safeArea.yMin / Screen.height);  // ������������� ����������� �������� ����� �� ���������
        rectTransform.anchorMax = new Vector2(1, (safeArea.yMin + safeAreaHeight) / Screen.height); // ������������ �������� ����� �� ���������
    }

    // --------------------

    public void Pause()
    {
        ScenesManager.Pause(this);
    }
}
