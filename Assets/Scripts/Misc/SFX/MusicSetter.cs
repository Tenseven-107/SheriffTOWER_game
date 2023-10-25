using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSetter : MonoBehaviour
{
    public bool startOnAwake = true; // Start on initialization
    public int song = 0; // Played song

    GameObject musicPlayer; // The music player object
    MusicPlayer mp; // The music player itself


    // Set up
    void Start()
    {
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        mp = musicPlayer.GetComponent<MusicPlayer>();

        musicPlayer.GetComponent<AudioSource>().volume = 1;
        
        if (startOnAwake) mp.SetSong(song);
    }


    // Play song
    public void Play()
    {
        mp.SetSong(song);
    }
}
