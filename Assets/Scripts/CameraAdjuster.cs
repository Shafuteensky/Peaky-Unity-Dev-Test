using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FitLocalObjectToOrthographicCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // Целевой объект как Transform для локальных координат

    private Camera orthographicCamera;

    void Start()
    {
        orthographicCamera = GetComponent<Camera>();
        if (targetObject != null)
        {
            FitObjectToCamera();
        }
        else
        {
            Debug.LogWarning("Целевой объект не назначен в FitLocalObjectToOrthographicCamera.");
        }
    }

    private void FitObjectToCamera()
    {
        // Получаем размеры объекта
        MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("Целевой объект не имеет MeshRenderer.");
            return;
        }

        // Используем локальные границы для расчетов
        Bounds localBounds = meshRenderer.bounds;

        // Получаем ось вращения объекта и угол наклона относительно камеры
        Vector3 objectForward = targetObject.forward;
        Vector3 cameraToObject = targetObject.position - orthographicCamera.transform.position;

        // Проекция объекта на плоскость, перпендикулярную камере
        Vector3 projectedObjectForward = Vector3.ProjectOnPlane(objectForward, orthographicCamera.transform.forward).normalized;

        // Вычисление угла между вектором направления объекта и вектором от камеры
        float angle = Vector3.Angle(objectForward, projectedObjectForward);

        // Корректируем размеры с учётом наклона
        float objectWidth = localBounds.size.x * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));
        float objectHeight = localBounds.size.y * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));

        // Центрируем камеру относительно локальной позиции объекта
        Vector3 localCenter = targetObject.localPosition + (localBounds.center - targetObject.position);
        orthographicCamera.transform.localPosition = new Vector3(
            localCenter.x,
            localCenter.y,
            orthographicCamera.transform.localPosition.z
        );

        // Расчет необходимого размера камеры
        float screenAspect = orthographicCamera.aspect;
        float objectAspect = objectWidth / objectHeight;

        if (screenAspect >= objectAspect)
        {
            // Камера шире, подстраиваем высоту
            orthographicCamera.orthographicSize = objectHeight / 2f;
        }
        else
        {
            // Камера выше, подстраиваем ширину
            orthographicCamera.orthographicSize = (objectWidth / screenAspect) / 2f;
        }
    }
}
