using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyWave> waves = new List<EnemyWave>();
        
    float currentCooldown = 1f;
    float waveCooldown = 15f;

    public int currentWaveCount = 0;
    EnemyWave currentWave;

    public int currentEnemyCount = 0;
    GameObject currentEnemy;
    public bool lastSpawned = false;

    public bool active = true;

    [SerializeField] Path path;
    GameOverScreen gameOverScreen;


    // Setup
    private void Start()
    {
        SetNewWave();
        StartCoroutine(WaveLoop());

        GameObject gameOverScreenObject = GameObject.FindGameObjectWithTag("GameOver");
        gameOverScreen = gameOverScreenObject.GetComponent<GameOverScreen>();
    }


    // Spawn a single enemy
    void SpawnEnemy()
    {
        if (currentEnemyCount != currentWave.enemies.Count)
        {
            currentEnemy = currentWave.enemies[currentEnemyCount];
            currentEnemyCount++;

            if (currentEnemy != null)
            {
                GameObject newEnemy = Instantiate(currentEnemy, transform);
                newEnemy.transform.position = path.GetFirst();

                PathFollower follower = newEnemy.GetComponent<PathFollower>();
                follower.SetPath(path);
            }

            if (currentEnemyCount == currentWave.enemies.Count) { lastSpawned = true; }
        }
    }



    // While a wave is active
    IEnumerator WaveLoop()
    {
        yield return new WaitForSeconds(currentCooldown);

        if (active == true)
        {
            if (lastSpawned == true && transform.childCount == 0)
            {
                active = false;

                if ((currentWaveCount + 1) != waves.Count)
                {
                    currentWaveCount++;
                    SetNewWave();
                }
            }
            else SpawnEnemy();
        }
        else { yield break; }

        StartCoroutine(WaveLoop());
    }



    // Remove enemies
    public void RemoveEnemies(bool setInactive)
    {
        if  (setInactive == true) 
        { 
            active = false;
            gameOverScreen.Activate();
        }

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }



    // STarting and setting waves
    void SetNewWave()
    {
        lastSpawned = false;
        currentEnemyCount = 0;

        currentWave = waves[currentWaveCount];
        currentCooldown = currentWave.cooldown;

        StartCoroutine(WaveCountdown());
    }


    IEnumerator WaveCountdown()
    {
        yield return new WaitForSeconds(waveCooldown);
        StartWaveSpawner();
        yield break;
    }


    public void StartWaveSpawner()
    {
        active = true;
        StartCoroutine(WaveLoop());
    }
}
