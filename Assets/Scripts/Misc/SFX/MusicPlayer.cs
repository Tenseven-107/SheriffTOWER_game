using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] musicClips; // List of songs
    AudioSource musicSource; // Audiosource of the player

    [Range( 0, 1)] public float volume = 1; // Volume of the music


    // Set up
    private void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        musicSource.loop = true;
        musicSource.volume = volume;

        if (gameObject.tag != "MusicPlayer") gameObject.tag = "MusicPlayer";

        CheckDoubles();
    }


    // Check if there's another musicplayer in the scene
    void CheckDoubles()
    {
        GameObject[] music_players = GameObject.FindGameObjectsWithTag("MusicPlayer");

        if (music_players.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }



    // Set the song
    public void SetSong(int song)
    {
        if (musicClips[song] != null && musicSource.clip != musicClips[song]) musicSource.clip = musicClips[song];
        else return;

        if (this.isActiveAndEnabled) musicSource.Play();
    }
}
