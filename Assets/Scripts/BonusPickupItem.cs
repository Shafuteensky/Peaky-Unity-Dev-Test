using UnityEngine;

public class BonusPickupItem : MonoBehaviour
{
    [field: SerializeField] public int boostPointsAmount { get; private set; }

    void Start()
    {
        boostPointsAmount = Random.Range(1, 100 + 1);

        // ������ ������ ������ � ����������� �� �����
        float minScale = 0.75f;
        float maxScale = 1.25f;
        float scaleFactor = Mathf.Lerp(minScale, maxScale, (boostPointsAmount - 1) / 99f);
        this.gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
