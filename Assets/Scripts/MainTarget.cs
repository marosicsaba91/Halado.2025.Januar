using UnityEngine;

public class MainTarget : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Agent agent))
        {
            agent.OnTargetReached();
        }
        
    }

}
