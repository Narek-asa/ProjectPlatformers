using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Tooltip("Reference to the camera transform.")]
    public Transform cameraTransform;

    [Tooltip("Multiplier for parallax effect. Values between 0 (no movement) and 1 (full movement).")]
    [Range(0f, 1f)]
    public float parallaxMultiplier = 0.5f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // Calculate how much the camera has moved since last frame
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Move the background by a fraction of the camera movement
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, deltaMovement.y * parallaxMultiplier, 0);

        // Update the last camera position
        lastCameraPosition = cameraTransform.position;
    }
}