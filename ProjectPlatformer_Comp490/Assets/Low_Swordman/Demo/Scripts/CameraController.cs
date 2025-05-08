using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // (optional) DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        // If we don't have a target yet, try to find the player by tag.
        if (Target == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                Target = playerGO;
            }
            else
            {
                // No player in scene yet, nothing to do this frame.
                return;
            }
        }

        // Now smoothly follow the assigned target.
        Vector3 targetPos = new Vector3(
            Target.transform.position.x,
            Target.transform.position.y + PosY,
            -100);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime);
    }
}


