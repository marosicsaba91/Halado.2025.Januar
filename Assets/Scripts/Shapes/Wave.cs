using UnityEngine;

[CreateAssetMenu]
public class Wave : LinearShape
{
    public Vector3 waveOffset = Vector3.right;
    public float waveCount = 1;

    public float startPhase;
    public float phaseChangeOverTime;

    [Range(0,1)] public float closeOnEnds;

    public override Vector3[] GetPoints(int pointCount)
    {
        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (pointCount - 1);

            float phase = startPhase + t * waveCount;
            
            if (Application.isPlaying)
                phase += phaseChangeOverTime * Time.time;

            float sin = Mathf.Sin(phase * 2 * Mathf.PI);

            float multiplier = Mathf.Sin(t * Mathf.PI);  // ... -1/2 Pi   -    + 3/2 Pi

            sin *= Mathf.Lerp(1, multiplier, closeOnEnds);

            points[i] = Vector3.Lerp(start, end, t) + sin * waveOffset;
        }
        return points;
    }
}