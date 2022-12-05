using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance = null;

    AudioSource audioSource;

    void OnEnable()
    {
        InputController.OnMutePress += ToggleMusic;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ToggleMusic()
    {
        audioSource.mute = !audioSource.mute;
    }

    public void PlaySong(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }

    void OnDisable()
    {
        InputController.OnMutePress -= ToggleMusic;
    }
}
