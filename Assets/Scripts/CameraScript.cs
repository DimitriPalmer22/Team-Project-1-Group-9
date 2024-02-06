using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class CameraScript : MonoBehaviour
{
    private const float FixedPointTransitionTime = .5f;

    /// <summary>
    /// A single instance of this script that represents the only camera script in the current scene 
    /// </summary>
    public static CameraScript Instance { get; private set; }

    /// <summary>
    /// The current mode that defines the camera's behavior 
    /// </summary>
    [SerializeField] private CameraMode cameraMode = CameraMode.Follow;

    public CameraMode CameraMode => cameraMode;

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

    /// <summary>
    /// Variable to keep track of if the camera is currently moving from one mode to another.
    /// </summary>
    private bool _transitioningPosition;

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
                if (!_transitioningPosition)
                    FollowTarget();
                break;
    
            case CameraMode.Fixed:
                if (!_transitioningPosition)
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
        // If there is no target object, skip
        if (targetObject == null)
            return;

        var oldPosition = transform.position;

        // Move the camera's y-value so that it always stays level
        transform.position = new Vector3(targetObject.position.x, cameraY, oldPosition.z);
    }

    /// <summary>
    /// Set the camera's position to the position of the fixed object.
    /// </summary>
    private void SetPositionToFixedPoint()
    {
        // If there is no fixed point, skip
        if (fixedPoint == null)
            return;

        var oldPosition = transform.position;
        var fixedPointPosition = fixedPoint.position;

        // Move the camera's position to the fixed point's position
        transform.position = new Vector3(fixedPointPosition.x, cameraY, oldPosition.z);
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
                StartCoroutine(TransitionToTarget());
                break;
            case CameraMode.Fixed:
                StartCoroutine(TransitionToFixedPoint());
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

    /// <summary>
    /// Coroutine to smoothly transition the camera's position to the fixed point
    /// </summary>
    IEnumerator TransitionToFixedPoint()
    {
        Vector2 startPosition = transform.position;

        float updatePrecision = Time.deltaTime;
        _transitioningPosition = true;
        
        for (float i = 0; i < FixedPointTransitionTime; i += updatePrecision)
        {
            var oldCameraPosition = transform.position;
            
            // Lerp the points to determine where the camera should be right now
            var lerpedPosition = Vector2.Lerp(startPosition, fixedPoint.position, i / FixedPointTransitionTime);
            
            transform.position = new Vector3(lerpedPosition.x, oldCameraPosition.y, oldCameraPosition.z);
            
            yield return new WaitForSeconds(updatePrecision);
        }

        _transitioningPosition = false;
    }

    /// <summary>
    /// Coroutine to smoothly transition the camera's position to the target
    /// </summary>
    IEnumerator TransitionToTarget()
    {
        Vector2 startPosition = transform.position;

        float updatePrecision = Time.deltaTime;
        _transitioningPosition = true;
        
        for (float i = 0; i < FixedPointTransitionTime; i += updatePrecision)
        {
            var oldCameraPosition = transform.position;
            
            // Lerp the points to determine where the camera should be right now
            var lerpedPosition = Vector2.Lerp(startPosition, targetObject.position, i / FixedPointTransitionTime);
            
            transform.position = new Vector3(lerpedPosition.x, oldCameraPosition.y, oldCameraPosition.z);
            
            yield return new WaitForSeconds(updatePrecision);
        }

        _transitioningPosition = false;
    }
}