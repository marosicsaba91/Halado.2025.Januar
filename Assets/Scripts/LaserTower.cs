using System;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class LaserTower : MonoBehaviour
{
    [SerializeField] float range = 3;
    [SerializeField] float damageRate = 10;
    [SerializeField] Transform startPoint;


    [Header("Laser visuals") ]
    [SerializeField] LineRenderer laserLine;
    [SerializeField, Min(2)] int PointCounts; 
    [SerializeField] List<SinWave> waves;
    [SerializeField] AnimationCurve distanceMultiplier;

    Vector3 StartPoint => startPoint.transform.position;

    Agent currentTarget;

    void Update()
    {
        if (currentTarget!= null && !TowerDefenseUtil.IsInRange(StartPoint, range, currentTarget))
            currentTarget = null;

        if (currentTarget == null)
            currentTarget = TowerDefenseUtil.FindClosest(StartPoint, range);
        
        if (currentTarget != null)
        {
            currentTarget.Damage(damageRate * Time.deltaTime);
            laserLine.enabled = true;

            SetLineRendererPoints();
        }
        else
        {
            laserLine.enabled = false;
        }
    }

    void SetLineRendererPoints()
    {
        laserLine.positionCount = PointCounts;

        Vector3 a = StartPoint;
        Vector3 b = currentTarget.TargetPoint;

        float dm = distanceMultiplier.Evaluate(Vector3.Distance(a, b));


        for (int i = 0; i < PointCounts; i++)
        {
            float t = i / (PointCounts - 1f);
            Vector3 p = Vector3.Lerp(a, b, t);

            /* EXTRA */

            float m = (Mathf.Cos((t - 0.5f) * 2 * Mathf.PI) + 1) / 2;

            foreach (var wave in waves)
                p += wave.Evaluate(Time.time, p.x) * m * dm;

            /* EXTRA */

            laserLine.SetPosition(i, p);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(StartPoint, range);
    }
}

[Serializable]
class SinWave
{
    public Vector3 amplitude = Vector3.up;
    public float frequency = 5;
    public float phase = 0;
    public float speed = 1;

    public Vector3 Evaluate(float time, float position)
    {
        return amplitude * Mathf.Sin(phase + frequency * time + position * speed);
    }
}
