using UnityEngine;

[CreateAssetMenu]
public class Spiral2 : LinearShape
{
    [SerializeField] float maxRadius = 1;

    public float waveCount = 1;

    public float startPhase;
    public float phaseChangeOverTime;

    [Range(0,1)] public float closeOnEnds;

    public override Vector3[] GetPoints(int pointCount)
    {
        Vector3 spineVector = (end - start).normalized;

        Vector3 other = (spineVector == Vector3.up || spineVector == Vector3.down) ? Vector3.right : Vector3.up;

        Vector3 direction1 = Vector3.Cross(spineVector, Vector3.up);
        Vector3 direction2 = Vector3.Cross(spineVector, direction1);

        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);

            float phase = startPhase + t * waveCount;
            
            if (Application.isPlaying)
                phase += phaseChangeOverTime * Time.time;

            float sin = Mathf.Sin(phase * 2 * Mathf.PI);
            float cos = Mathf.Cos(phase * 2 * Mathf.PI);
            float r = maxRadius;

            float multiplier = Mathf.Sin(t * Mathf.PI);  // ... -1/2 Pi   -    + 3/2 Pi

            r *= Mathf.Lerp(1, multiplier, closeOnEnds);
            Vector3 fullOffset = sin * direction1 + cos * direction2;

            points[i] = Vector3.Lerp(start, end, t) + fullOffset * r;
        }
        return points;
    }
}