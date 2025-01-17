using System.Collections;
using UnityEngine;

public class BonusPickupItem : MonoBehaviour
{
    [field: SerializeField] public int boostPointsAmount { get; private set; }
    private float pickupAnimationDuration = 0.75f;

    void Start()
    {
        boostPointsAmount = Random.Range(1, 100 + 1);

        // ������ ������ ������ � ����������� �� �����
        float minScale = 0.75f;
        float maxScale = 1.25f;
        float scaleFactor = Mathf.Lerp(minScale, maxScale, (boostPointsAmount - 1) / 99f);
        this.gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    // =========================

    public void PickItUp()
    {
        StartCoroutine(PickupAnimation());
    }

    // �������� �������
    private IEnumerator PickupAnimation()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 enlargedScale = originalScale * 1.2f;
        float elapsedTime = 0f;

        // ���������� �������
        while (elapsedTime < pickupAnimationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, enlargedScale, elapsedTime / (pickupAnimationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����� ������� ��� ���� ����������
        elapsedTime = 0f;
        while (elapsedTime < pickupAnimationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(enlargedScale, Vector3.zero, elapsedTime / (pickupAnimationDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����������� ������� ����� ���������� ��������
        Destroy(gameObject);
    }
}
