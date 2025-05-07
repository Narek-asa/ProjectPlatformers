using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public string nextSceneName; // Assign this in the inspector

    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the one you want to trigger the scene change
        if (other.gameObject.CompareTag("Player")) // Example: Check if it's the player
        {
            SceneManager.LoadScene(nextSceneName); // Load the scene
        }
    }
}
