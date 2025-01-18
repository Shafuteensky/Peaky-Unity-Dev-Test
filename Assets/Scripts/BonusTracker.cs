using System.Linq;
using TMPro;
using UnityEngine;

public class BonusTracker : MonoBehaviour
{
    [SerializeField] private GameObject bonusSumPanel;
    [SerializeField] private GameObject pointSumPanel;
    private TextMeshProUGUI bonusSumText;
    private TextMeshProUGUI pointSumText;

    public int totalPickupsCount = 0;
    public int totalBoostPoints = 0;
    public float averageBoostPoints = 0;

    private void Awake()
    {
        bonusSumText = bonusSumPanel.GetComponent<TextMeshProUGUI>();
        pointSumText = pointSumPanel.GetComponent<TextMeshProUGUI>();

        bonusSumText.text = "0";
        pointSumText.text = "0";
    }

    // =================================

    // Зачисление бонусов при поднятии
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickups"))
        {
            other.gameObject.tag = "Untagged";

            BonusPickupItem pickup = other.GetComponent<BonusPickupItem>();
            if (pickup != null)
            {
                totalBoostPoints += pickup.boostPointsAmount;
                totalPickupsCount++;
                averageBoostPoints = (float)totalBoostPoints / totalPickupsCount;

                pickup.PickItUp();
                this.gameObject.GetComponent<GameTime>()?.AddTime(5f);

                bonusSumText.text = totalPickupsCount.ToString();
                pointSumText.text = totalBoostPoints.ToString();
            }

            // Все ли бонусы на карте поднятны
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickups");
            if (pickups != null && !pickups.Any())
                ScenesManager.SaveScoreAndLoadGameOver();
        }
    }
}
