using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f; 
    public float minX = -7, maxX = 7; // Ограничения по горизонтали (по оси X)

    void Update()
    {
        Vector3 newPosition;
        if (Screen.width > Screen.height) // В альбомной ориентации ничего не делаем
        {
            newPosition = new Vector3(0, transform.position.y, transform.position.z);
            transform.position = newPosition;
            return;
        }

        float newX = Mathf.Clamp(player.position.x, minX, maxX);
        newPosition = new Vector3(newX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }
}
