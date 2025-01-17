using UnityEngine;

public class BonusTracker : MonoBehaviour
{
    private int totalPickupsCount = 0;
    public int totalBoostPoints = 0;
    public float averageBoostPoints = 0;   

    // Зачисление бонусов при поднятии
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickups"))
        {
            BonusPickupItem pickup = other.GetComponent<BonusPickupItem>();
            if (pickup != null)
            {
                totalBoostPoints += pickup.boostPointsAmount;
                totalPickupsCount++;
                averageBoostPoints = (float)totalBoostPoints / totalPickupsCount;

                pickup.PickItUp();
                this.gameObject.GetComponent<GameTime>()?.AddTime(5f);
            }
        }
    }
}
