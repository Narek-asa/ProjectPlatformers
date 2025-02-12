using UnityEngine;

public class CameraControlMain : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("The target that the camera will follow (assign the player's transform here).")]
    public Transform target;

    [Tooltip("Offset applied to the target's position. Adjust Y to change vertical view, and Z for camera distance.")]
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    [Header("Smoothing Settings")]
    [Tooltip("How long (in seconds) it takes for the camera to catch up to the target. Higher values = more delay.")]
    public float smoothTime = 0.3f;

    // Internal variable used by SmoothDamp to track the current velocity.
    private Vector3 velocity = Vector3.zero;

    // LateUpdate ensures the camera updates after the player has moved.
    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraController: No target assigned.");
            return;
        }

        // Calculate the target position based on the player's position plus the offset.
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera from its current position to the target position.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}