using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField] float startHP = 100;
    [SerializeField] RectTransform healthBar;
    [SerializeField] Transform targetTransform;
    [SerializeField] NavMeshAgent navMeshAgent;

    public static List<Agent> allAgents = new();

    public Vector3 TargetPoint => targetTransform.position;

    public float HP { get; private set; }

    void Start()
    {
        HP = startHP;
        FreshUI();
    }

    public void Damage(float damage) 
    {
        HP -= damage;
        FreshUI();

        if (HP <= 0)
            Destroy(gameObject);    
    }

    void FreshUI()
    {
        float healthRate = HP / startHP;
        Vector2 am = healthBar.anchorMin;
        healthBar.anchorMin = new( 1 - healthRate , am.y);
    }

    void OnEnable()
    {
        allAgents.Add(this);
    }

    void OnDisable()
    {
        allAgents.Remove(this);
    }

    public void OnTargetReached()
    {
        GameManager gm = GameManager.Instance;   // Gyorsabb
        // gm.Life = gm.Life + 1;

        gm.Life--;

        Destroy(gameObject);
    }

    public float GetTargetDistance() => navMeshAgent.remainingDistance;
}