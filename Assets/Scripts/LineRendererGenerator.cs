using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererGenerator : MonoBehaviour
{
    public int pointCount = 32;
    public float radius = 1f;

    [HideInInspector]
    public LineRenderer lineRenderer;

    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void GenerateCircle()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointCount + 1;

        for (int i = 0; i <= pointCount; i++)
        {
            float angle = i * Mathf.PI * 2f / pointCount;
            Vector3 point = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            lineRenderer.SetPosition(i, point);
        }

        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false; // So it stays local to the object
    }
}
