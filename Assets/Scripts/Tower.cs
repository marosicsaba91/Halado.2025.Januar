using System;
using UnityEngine;

public enum Limit { Min, Max }

public enum TargetSelection
{
    ClosestToTower,
    FurthestFromTower,
    ClosestToGoal,
    FurthestFromGoal,
    LowestHP,
    HighestHP,
}

[SelectionBase]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float range = 3;
    [SerializeField] protected TargetSelection targetSelection;
    [SerializeField] Transform startPoint;

    protected Vector3 StartPoint => startPoint.transform.position;

    protected Agent FindTarget() => targetSelection switch
    {
        TargetSelection.ClosestToTower => FindClosest(StartPoint, range),
        TargetSelection.FurthestFromTower => FindFurthest(StartPoint, range),
        TargetSelection.ClosestToGoal => FindClosestToGoal(StartPoint, range),
        TargetSelection.FurthestFromGoal => FindFurthestFromGoal(StartPoint, range),
        TargetSelection.LowestHP => FindLowestHP(StartPoint, range),
        TargetSelection.HighestHP => FindHighestHP(StartPoint, range),
        _ => throw new System.NotImplementedException($"TargetSelection {targetSelection} is not Implemented!"),
    };


    public static Agent FindClosest(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Min, (agent) => Vector3.Distance(position, agent.TargetPoint));

    public static Agent FindFurthest(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Max, (agent) => Vector3.Distance(position, agent.TargetPoint));

    public static Agent FindClosestToGoal(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Max, (agent) => agent.GetTargetDistance());

    public static Agent FindFurthestFromGoal(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Max, (agent) => agent.GetTargetDistance());

    public static Agent FindHighestHP(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Max, (agent) => agent.HP);

    public static Agent FindLowestHP(Vector3 position, float maxRange) =>
        FindAgent(position, maxRange, Limit.Min, agent => agent.HP);



    static Agent FindAgent(Vector3 position, float maxRange, Limit limit, Func<Agent, float> func)
    {
        Agent target = null;
        float bestValue = limit == Limit.Min ? float.MaxValue : float.MinValue;

        foreach (Agent agent in Agent.allAgents)
        {
            float distanceFromTower = Vector3.Distance(position, agent.TargetPoint);
            if (distanceFromTower > maxRange)
                continue;

            float value = func(agent);
            if ((limit == Limit.Min && value < bestValue) || (limit == Limit.Max && value > bestValue))
            {
                target = agent;
                bestValue = value;
            }
        }

        return target;
    }

    public static bool IsInRange(Vector3 position, float maxRange, Agent agent)
    {
        float distance = Vector3.Distance(position, agent.TargetPoint);
        return distance <= maxRange;
    }


    void OnDrawGizmosSelected()
    {
        if (startPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(StartPoint, range);
    }
}