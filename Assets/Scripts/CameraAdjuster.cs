using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FitLocalObjectToOrthographicCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // ������� ������ ��� Transform ��� ��������� ���������

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
            Debug.LogWarning("������� ������ �� �������� � FitLocalObjectToOrthographicCamera.");
        }
    }

    private void FitObjectToCamera()
    {
        // �������� ������� �������
        MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("������� ������ �� ����� MeshRenderer.");
            return;
        }

        // ���������� ��������� ������� ��� ��������
        Bounds localBounds = meshRenderer.bounds;

        // �������� ��� �������� ������� � ���� ������� ������������ ������
        Vector3 objectForward = targetObject.forward;
        Vector3 cameraToObject = targetObject.position - orthographicCamera.transform.position;

        // �������� ������� �� ���������, ���������������� ������
        Vector3 projectedObjectForward = Vector3.ProjectOnPlane(objectForward, orthographicCamera.transform.forward).normalized;

        // ���������� ���� ����� �������� ����������� ������� � �������� �� ������
        float angle = Vector3.Angle(objectForward, projectedObjectForward);

        // ������������ ������� � ������ �������
        float objectWidth = localBounds.size.x * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));
        float objectHeight = localBounds.size.y * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad));

        // ���������� ������ ������������ ��������� ������� �������
        Vector3 localCenter = targetObject.localPosition + (localBounds.center - targetObject.position);
        orthographicCamera.transform.localPosition = new Vector3(
            localCenter.x,
            localCenter.y,
            orthographicCamera.transform.localPosition.z
        );

        // ������ ������������ ������� ������
        float screenAspect = orthographicCamera.aspect;
        float objectAspect = objectWidth / objectHeight;

        if (screenAspect >= objectAspect)
        {
            // ������ ����, ������������ ������
            orthographicCamera.orthographicSize = objectHeight / 2f;
        }
        else
        {
            // ������ ����, ������������ ������
            orthographicCamera.orthographicSize = (objectWidth / screenAspect) / 2f;
        }
    }
}
