using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Keep track of the game object to follow
    [SerializeField] private Transform targetObject;

    // the y-value of the camera's position
    [SerializeField] private float cameraY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // move the camera's position
        FollowTarget();
    }

    /// <summary>
    /// Move the camera's transform based on where the target is
    /// </summary>
    private void FollowTarget()
    {
        var oldPosition = transform.position;
        
        // Move the camera's y-value so that it always stays level
        transform.position = new Vector3(targetObject.position.x, cameraY, oldPosition.z);
    }
}
