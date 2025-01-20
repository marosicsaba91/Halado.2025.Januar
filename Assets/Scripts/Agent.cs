using System;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] float startHP = 100;
    [SerializeField] RectTransform healthBar;
    [SerializeField] Transform targetTransform;

    public static List<Agent> allAgents = new();

    public Vector3 TargetPoint => targetTransform.position;

    float hp;

    void Start()
    {
        hp = startHP;
        FreshUI();
    }

    public void Damage(float damage) 
    {
        hp -= damage;
        FreshUI();

        if (hp <= 0)
            Destroy(gameObject);    
    }

    void FreshUI()
    {
        float healthRate = hp / startHP;
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
        GameManager gm = GameManager.Instance;

        // gm.Life = gm.Life + 1;

        gm.Life--;

        Destroy(gameObject);
    }
}