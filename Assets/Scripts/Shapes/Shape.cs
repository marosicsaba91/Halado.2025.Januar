
using UnityEngine;

public abstract class Shape : ScriptableObject
{
    public abstract Vector3[] GetPoints(int pointCount);
}