using UnityEngine;

public class ScoreTableHandler : MonoBehaviour
{
    [SerializeField] private GameObject scoreLineAsset;

    private void Awake()
    {
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);
    }
}
