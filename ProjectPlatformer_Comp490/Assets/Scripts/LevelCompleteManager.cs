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

    public void MainMenu()
    {
        if (MusicManager.Instance != null)
        {
            Destroy(MusicManager.Instance.gameObject);
        }
        
        SceneManager.LoadScene("Main Menu");
    }
}
