using UnityEngine;

public class CameraZoneTrigger : MonoBehaviour
{
    [Tooltip("The name of the camera to switch to.")]
    public string targetCameraName; // Set this to "Camera1" or "Camera2"

    private GameObject cameraManager;

    private void Start()
    {
        // Find CameraManager in the scene
        cameraManager = GameObject.Find("CameraManager");

        if (cameraManager == null)
        {
            Debug.LogError("CameraManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure only the player triggers the camera switch
        if (other.CompareTag("Player"))
        {
            SwitchToTargetCamera();
        }
    }

    void SwitchToTargetCamera()
    {
        // Find all cameras under CameraManager
        Camera[] allCameras = cameraManager.GetComponentsInChildren<Camera>();

        // Disable all cameras first
        foreach (Camera cam in allCameras)
        {
            cam.enabled = false;
        }

        // Find the target camera by its name and enable it
        Transform targetCameraTransform = cameraManager.transform.Find(targetCameraName);
        if (targetCameraTransform != null)
        {
            Camera targetCamera = targetCameraTransform.GetComponent<Camera>();
            if (targetCamera != null)
            {
                targetCamera.enabled = true; // Enable the target camera
            }
            else
            {
                Debug.LogError("No Camera component found on " + targetCameraName);
            }
        }
        else
        {
            Debug.LogError("Target camera named '" + targetCameraName + "' not found in CameraManager.");
        }
    }
}
