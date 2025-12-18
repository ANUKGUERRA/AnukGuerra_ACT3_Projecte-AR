using UnityEngine;

public class detectPosition : MonoBehaviour
{
    [SerializeField] GameObject hexGrid;
    public static bool DetectedHex;
    public static RaycastHit hit;

    LineRenderer lineRenderer;
    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.02f;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            DetectedHex = true;
        }
        else
        {
            lineRenderer.enabled = true;
            DetectedHex = false;
        }
    }
}