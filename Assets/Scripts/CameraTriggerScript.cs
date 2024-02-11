using UnityEngine;

/// <summary>
/// A script used to change the camera's mode throughout the level
/// </summary>
public class CameraTriggerScript : MonoBehaviour
{

    public CameraMode mode;
    
    /// <summary>
    /// The point that the camera fixes to if it switches to the fixed state
    /// </summary>
    public Transform fixedPoint;

    /// <summary>
    /// Bool to restrict the trigger from activating more than once
    /// </summary>
    private bool _hasBeenActivated;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only allow triggers to be activated once
        if (_hasBeenActivated)
            return;
        
        // Do not run this code if something other than the player enters this trigger
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        // get the instance of the camera script
        var cameraScript = CameraScript.Instance;

        // update the camera's values to trigger's values
        switch (mode)
        {
            case CameraMode.Follow:
                cameraScript.ChangeCameraMode(CameraMode.Follow);
                break;
            case CameraMode.Fixed:
                cameraScript.SetFixedPointAndChangeMode(fixedPoint);
                break;
            default:
                Debug.LogError($"\"{mode}\" CASE IS NOT HANDLED IN THE CAMERA TRIGGER SCRIPT!");
                break;
        }
        
        // Indicate that the trigger has been activated at least once
        _hasBeenActivated = true;
        
        // // Debug stuff. Remove later
        // if (cameraScript.CameraMode == CameraMode.Fixed)
        //     cameraScript.ChangeCameraMode(CameraMode.Follow);
        // else
        //     cameraScript.SetFixedPointAndChangeMode(fixedPoint);
        
    }
}
