using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Объект для случайного спавна")]
    [SerializeField] private GameObject assetToSpawn;

    [Header("Настройки спавна")]
    [SerializeField] private int minAmount = 5;
    [SerializeField] private int maxAmount = 10;
    private float maxSpawnHeight = 10f;
    private float sphereCastRadius = 0.5f;
    [SerializeField] private float offsetY = 1f; // Смещение по Y

    [Header("Область спавна")]
    [SerializeField] private Vector2 areaCenter = Vector2.zero;
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 10f);

    private void Start()
    {
        SpawnObjects();
    }

    // =========================================

    /// <summary>
    /// Случайная генерация объектов по области
    /// </summary>
    public void SpawnObjects()
    {
        if (assetToSpawn == null)
            return;

        // Случайное количество к спавну
        int spawnNumber = Random.Range(minAmount, maxAmount + 1);

        // Генерация объектов в случайных местах
        for (int i = 0; i < spawnNumber; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                spawnPosition.y += offsetY; // Смещение по Y над террейном
                Instantiate(assetToSpawn, spawnPosition, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
            }    
        }
    }

    /// <summary>
    /// Случайная точка в рамках заданной квадратной области с коррекцией по высоте (Y)
    /// </summary>
    /// <returns>Точка в пространстве или нулевая позиция, если нет объекта/террейна слоя Default</returns>
    private Vector3 GetSpawnPosition()
    {
        Vector2 flatPosition = GetRandomPositionInArea();
        Vector3 startPosition = new Vector3(flatPosition.x, maxSpawnHeight, flatPosition.y);

        if (Physics.SphereCast(startPosition, sphereCastRadius, Vector3.down, out RaycastHit hit, 
            maxSpawnHeight * 2, LayerMask.GetMask("Default")))
            return hit.point;

        // Если место не найдено, возвращаем нулевую позицию
        return Vector3.zero; 
    }

    /// <summary>
    /// Случайная точка в рамках заданной квадратной области
    /// </summary>
    private Vector2 GetRandomPositionInArea()
    {
        float x = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float y = Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);
        return new Vector2(x, y);
    }

    //
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(areaCenter.x, 0, areaCenter.y), new Vector3(areaSize.x, 0, areaSize.y));
    }
}
