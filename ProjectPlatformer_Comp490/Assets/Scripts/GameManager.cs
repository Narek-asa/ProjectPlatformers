using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    public void RespawnPlayer()
    {
        // Check to see if player has GameManager script and player is assigned
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Reset the player's health and position to "Respawn"
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
}