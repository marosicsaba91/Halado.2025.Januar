using UnityEngine;

public static class TowerDefenseUtil
{
    public static Agent FindClosest(Vector3 position, float maxRange)
    {
        Agent target = null;
        float minDistance = float.MaxValue;

        foreach (Agent agent in Agent.allAgents)
        {
            float distance = Vector3.Distance(position, agent.TargetPoint);
            if (distance <= maxRange && distance <= minDistance)
            {
                target = agent;
                minDistance = distance;
            }
        }

        return target;
    }

    public static bool IsInRange(Vector3 position, float maxRange, Agent agent)
    {
        float distance = Vector3.Distance(position, agent.TargetPoint);
        return distance <= maxRange;
    }
}