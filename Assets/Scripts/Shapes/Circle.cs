using UnityEngine;

[CreateAssetMenu]
public class Circle : Shape
{
    public Vector3 center = Vector3.zero;
    public float radius = 1;

    public override Vector3[] GetPoints(int pointCount)
    {
        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float angel = i * 2 * Mathf.PI / pointCount;

            Vector3 direction = new(Mathf.Cos(angel), Mathf.Sin(angel));
            points[i] = center + direction * radius;
        }
        return points;
    }
}