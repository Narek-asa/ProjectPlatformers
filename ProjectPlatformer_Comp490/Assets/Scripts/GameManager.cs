using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    public void RespawnPlayer()
    {
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Respawn the player at the current spawnPoint
                playerHealth.ResetPlayer(spawnPoint.position);
            }
            else
            {
                Debug.LogError("Player does not have a Health component!");
            }
        }
        else
        {
            Debug.LogError("No player assigned in GameManager!");
        }
    }

    // This method updates the spawn point.
    public void UpdateSpawnPoint(Transform newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
        Debug.Log("Spawn point updated to: " + newSpawnPoint.position);
    }
}