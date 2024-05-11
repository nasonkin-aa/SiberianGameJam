using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D startObject;  // ��������� ������
    public Rigidbody2D endObject;    // �������� ������
    public float ropeLength = 10f;  // ����� �������
    public float ropeWidth = 0.1f;   // ������ �������

    private LineRenderer lineRenderer;

    void Start()
    {
        // ��������� ��������� DistanceJoint2D � ���������� �������
        DistanceJoint2D joint = startObject.gameObject.AddComponent<DistanceJoint2D>();

        // ������������� �������� DistanceJoint2D
        joint.connectedBody = endObject;
        joint.distance = ropeLength;
        joint.maxDistanceOnly = true;
        joint.autoConfigureDistance = false;

        // ��������� ��������� LineRenderer � ���������� �������
        lineRenderer = startObject.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        lineRenderer.positionCount = 2;
        /*lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;*/
    }

    void Update()
    {
        // ��������� ����� ��������� ��� LineRenderer
        lineRenderer.SetPosition(0, startObject.transform.position);
        lineRenderer.SetPosition(1, endObject.transform.position);
    }
}
