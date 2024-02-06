using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    /// <summary>
    /// An enum used to list the different modes the camera can be in
    /// </summary>
    public enum CameraMode
    {
        // Follow the player
        Follow,
        
        // Stay static at a fixed point
        Fixed 
    }
    
    /// <summary>
    /// A single instance of this script that represents the only camera script in the current scene 
    /// </summary>
    public static CameraScript Instance { get; private set; }

    /// <summary>
    /// The current mode that defines the camera's behavior 
    /// </summary>
    [SerializeField] private CameraMode cameraMode = CameraMode.Follow;

    /// <summary>
    /// The transform to follow when the camera is in "Follow" mode 
    /// </summary>
    [SerializeField] private Transform targetObject;

    /// <summary>
    /// The fixed point to stay at when the camera is in "Fixed" mode 
    /// </summary>
    [SerializeField] private Transform fixedPoint;

    /// <summary>
    /// The y-value of the camera's position 
    /// </summary>
    [SerializeField] private float cameraY;


    // Start is called before the first frame update
    void Start()
    {
        // Set the instance if there is not one already in place
        if (Instance == null)
            Instance = this;
    }

    // The camera uses late update to make sure the camera updates its position after all other
    // game objects are done moving.
    void LateUpdate()
    {
        // move the camera's position based on the current camera mode
        switch (cameraMode)
        {
            case CameraMode.Follow:
                FollowTarget();
                break;
            case CameraMode.Fixed:
                SetPositionToFixedPoint();
                break;
            default:
                Debug.LogError($"\"{cameraMode}\" CASE IS NOT HANDLED IN THE CAMERA SCRIPT!");
                break;
        }
    }

    /// <summary>
    /// Move the camera's transform based on where the target is
    /// </summary>
    private void FollowTarget()
    {
        if (targetObject == null)
            return;
        
        var oldPosition = transform.position;

        // Move the camera's y-value so that it always stays level
        transform.position = new Vector3(targetObject.position.x, cameraY, oldPosition.z);
    }

    private void SetPositionToFixedPoint()
    {
        if (fixedPoint == null)
            return;
        
        var oldPosition = transform.position;
        var fixedPointPosition = fixedPoint.position;

        // Move the camera's position to the fixed point's position
        transform.position = new Vector3(fixedPointPosition.x, fixedPointPosition.y, oldPosition.z);
    }
    
    /// <summary>
    /// Change the current camera mode
    /// </summary>
    /// <param name="mode">The mode to switch the camera to.</param>
    public void ChangeCameraMode(CameraMode mode)
    {
        // if the new camera mode is the same as the current camera mode, skip.
        if (mode == cameraMode)
            return;

        switch (mode)
        {
            case CameraMode.Follow:
                break;
            case CameraMode.Fixed:
                break;
            default:
                Debug.LogError($"\"{mode}\" CASE IS NOT HANDLED IN THE CAMERA SCRIPT!");
                break;
        }

        cameraMode = mode;
    }

    /// <summary>
    /// Set this script's fixed point and change the camera mode to "Fixed"
    /// </summary>
    /// <param name="point">The new fixed point</param>
    public void SetFixedPointAndChangeMode(Transform point)
    {
        fixedPoint = point;
        ChangeCameraMode(CameraMode.Fixed);
    }
    
    
}