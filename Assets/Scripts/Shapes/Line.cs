using UnityEngine;
 
[CreateAssetMenu]
public class Line : LinearShape
{
    public override Vector3[] GetPoints(int pointCount)
    {
        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);
            points[i] = Vector3.Lerp(start, end, t);
        }
        return points;
    }
}
