using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Call this method to restart music
    public void ResetMusic()
    {
        if(audioSource != null)
        {
            audioSource.Stop();
            audioSource.time = 0;
            audioSource.Play();
        }
    }
}