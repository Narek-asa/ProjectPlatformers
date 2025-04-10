using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Tooltip("Level Complete Panel's UI Manager.")]
    [SerializeField] private LevelCompleteUIManager levelCompleteUIManager;

    [Tooltip("Background Object.")]
    [SerializeField] private DimBackgroundFader dimBackgroundFader;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            else
            {
                Debug.LogWarning("Goal: PlayerMovement component not found on the player.");
            }

            // Show Level Complete UI
            if (levelCompleteUIManager != null)
                levelCompleteUIManager.ShowLevelCompleteScreen();

            // Fade in the background
            if (dimBackgroundFader != null)
                dimBackgroundFader.FadeIn();
            else
                Debug.LogWarning("Goal: DimBackgroundFader is not assigned.");
        }
    }
}
