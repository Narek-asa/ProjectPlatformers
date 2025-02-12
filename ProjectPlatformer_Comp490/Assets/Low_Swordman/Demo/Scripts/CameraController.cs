using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Tooltip("The target for the camera to follow")]
    public GameObject Target;

    [Tooltip("Time for the camera to reach the target position")]
    [SerializeField] private float smoothTime = 0.5f;

    [Tooltip("Vertical offset from the target's position")]
    public float PosY = 1f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (Target == null)
            return;

        // Base target position
        Vector3 targetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + PosY, -100);

        // Smoothly move the camera toward the target position
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        transform.position = newPos;
    }
}

