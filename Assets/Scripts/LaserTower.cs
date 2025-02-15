using UnityEngine;

[SelectionBase]
public class LaserTower : Tower
{
    [Header("Laser")]
    [SerializeField] AnimationCurve damageRateOverTime;

    [Header("Visuals")]
    [SerializeField] LineRenderer laserLine;
    [SerializeField] LinearShape lineShape;
    [SerializeField, Min(2)] int pointCount;
    
    Agent currentTarget;
    float damageTime = 0;

    void Update()
    {
        if (currentTarget != null && !IsInRange(StartPoint, range, currentTarget))
        {
            currentTarget = null;
            damageTime = 0;
        }

        if (currentTarget == null)
        {
            currentTarget = FindTarget();
            damageTime = 0;
        }

        if (currentTarget != null)
        {
            float damageRate = damageRateOverTime.Evaluate(damageTime);
            damageTime += Time.deltaTime;

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
        laserLine.positionCount = pointCount;

        lineShape.start = StartPoint;
        lineShape.end = currentTarget.TargetPoint;

        laserLine.SetPositions(lineShape.GetPoints(pointCount));
    }
}