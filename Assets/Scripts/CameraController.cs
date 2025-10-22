using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float minX, maxX, minY, maxY;
    
    void LateUpdate()
    {
        float x = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        float y = Mathf.Clamp(playerTransform.position.y, minY, maxY);
        Vector3 newPosition = new Vector3(x, y + cameraOffset.y, cameraOffset.z);
       transform.position = newPosition; 
    }
}
