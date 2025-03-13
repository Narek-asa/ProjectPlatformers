using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform checkpointSpawnPoint;

    private void Start()
    {
        if (checkpointSpawnPoint == null)
        {
            checkpointSpawnPoint = transform;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.UpdateSpawnPoint(checkpointSpawnPoint);
                Debug.Log("Checkpoint activated. Spawn point updated to: " + checkpointSpawnPoint.position);
            }
            else
            {
                Debug.LogError("No GameManager found in the scene!");
            }
        }
    }
}