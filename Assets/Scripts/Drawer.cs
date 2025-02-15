using UnityEngine;

[ExecuteAlways]
public class Drawer : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Shape shape;
    [SerializeField] int pointCount = 10;

    void Update()
    {
        if (shape == null)
            return;

        Vector3[] points = shape.GetPoints(pointCount);

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
