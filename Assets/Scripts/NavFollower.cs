using UnityEngine;
using UnityEngine.AI;

public class NavFollower : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    MainTarget target;

    void Start()
    {
        target = FindAnyObjectByType<MainTarget>(); 
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
