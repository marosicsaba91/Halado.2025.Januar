using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 cameraDirection = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(cameraDirection);        
    }
}
