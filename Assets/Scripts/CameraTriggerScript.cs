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
        // Do not run this code if something other than the player enters this trigger
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        // get the instance of the camera script
        var cameraScript = CameraScript.Instance;

        // update the camera's values to trigger's values
        switch (mode)
        {
            case CameraMode.Follow:
                break;
            case CameraMode.Fixed:
                break;
            default:
                Debug.LogError($"\"{mode}\" CASE IS NOT HANDLED IN THE CAMERA TRIGGER SCRIPT!");
                break;
        }
        
        // Debug stuff. Remove later
        if (cameraScript.CameraMode == CameraMode.Fixed)
            cameraScript.ChangeCameraMode(CameraMode.Follow);
        else
            cameraScript.SetFixedPointAndChangeMode(fixedPoint);
        
    }
}
