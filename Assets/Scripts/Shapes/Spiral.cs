using UnityEngine;

[CreateAssetMenu]
public class Spiral : LinearShape
{
    public float radius;
    public float waveCount = 1;
    public float startPhase = 0;
    public float phaseOverTime = 0;

    [Range(0, 1)] public float closeEnd = 1;

    public override Vector3[] GetPoints(int pointCount)
    {
        Vector3[] points = new Vector3[pointCount];

        Vector3 dir = (end - start).normalized;
        Vector3 perpendicular1, perpendicular2;

        if (dir == Vector3.up || dir == Vector3.down)
        {
            perpendicular1 = Vector3.right;
            perpendicular2 = Vector3.forward; 
        }
        else
        {
            perpendicular1 = Vector3.Cross(dir, Vector3.up);
            perpendicular2 = Vector3.Cross(dir, perpendicular1);
        }

        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);
            float phase = Application.isPlaying ?
                startPhase + Time.time * phaseOverTime
                :startPhase;

            float phaseRad = (phase + t * waveCount) * 2 * Mathf.PI;
            float sin = Mathf.Sin(phaseRad);
            float cos = Mathf.Cos(phaseRad);


            float multiplier = Mathf.Lerp(1, Mathf.Sin(t * Mathf.PI), closeEnd);
            Vector3 center = Vector3.Lerp(start, end, t);

            points[i] = center + (((sin * perpendicular1) + (cos * perpendicular2)) * multiplier * radius);
        }
        return points;
    }
}