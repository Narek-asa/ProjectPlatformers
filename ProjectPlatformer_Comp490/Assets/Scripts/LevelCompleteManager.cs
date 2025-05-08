using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public void RestartLevel()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.ResetMusic();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // find the camera controller
        var cam = CameraController.Instance;
        if (cam != null)
        {
            var playerGo = GameObject.FindGameObjectWithTag("Player");
            if (playerGo != null)
                cam.Target = playerGo;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void MainMenu()
    {
        if (MusicManager.Instance != null)
        {
            Destroy(MusicManager.Instance.gameObject);
        }

        SceneManager.LoadScene("Main Menu");
    }
}
