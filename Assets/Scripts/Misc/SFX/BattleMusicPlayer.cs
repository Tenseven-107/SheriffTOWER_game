using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleMusicPlayer : MonoBehaviour
{
    // Plays battlemusic and fades it when the wave is over, also fades main musicplayer

    [SerializeField] float fadeDuration = 0.5f; // Duration of fade object

    public AudioClip musicClip; // List of songs
    AudioSource musicSource; // Audiosource of the player

    [SerializeField] EnemySpawner spawner; // Enemyspawner
    AudioSource musicPlayer; // Normal music player


    // Set up
    private void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        GameObject MSObject = GameObject.FindGameObjectWithTag("MusicPlayer");
        musicPlayer = MSObject.GetComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 0;

        musicSource.clip = musicClip;
        musicSource.Play();
    }


    // Fades battle music and normal musicplayers in and out when necessary
    private void Update()
    {
        if (spawner.active == true)
        {
            if (musicSource.volume == 0)
            {
                StartCoroutine(StartFade(musicSource, fadeDuration, 1));
                StartCoroutine(StartFade(musicPlayer, fadeDuration, 0));

            }
        }
        else
        {
            if (musicSource.volume != 0)
            {
                StartCoroutine(StartFade(musicSource, fadeDuration, 0));
                StartCoroutine(StartFade(musicPlayer, fadeDuration, 1));
            }
        }
    }

    // Fade effect
    IEnumerator StartFade(AudioSource source, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = source.volume;
        while (currentTime < duration) // fades until the current time is higher then the duration value of the fade
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
