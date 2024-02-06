using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A script used to change the camera's mode throughout the level
/// </summary>
public class CameraTriggerScript : MonoBehaviour
{

    public CameraMode mode;
    public Transform fixedPoint;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"ENTERED: {other.gameObject.tag}");
        
        if (other.gameObject.tag != "Player")
            return;
        
        var cameraScript = CameraScript.Instance;

        
        if (cameraScript.CameraMode == CameraMode.Fixed)
            cameraScript.ChangeCameraMode(CameraMode.Follow);
        else
            cameraScript.SetFixedPointAndChangeMode(fixedPoint);
        
    }
}
