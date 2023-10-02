using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyWave> waves = new List<EnemyWave>();
        
    float currentCooldown = 1f;

    public int currentWaveCount = 0;
    EnemyWave currentWave;

    int currentEnemyCount = 0;
    GameObject currentEnemy;
    public bool lastSpawned = false;

    bool active = true;

    [SerializeField] Path path;



    private void Start()
    {
        SetNewWave();
        StartCoroutine(WaveLoop());
    }


    void SpawnEnemy()
    {
        currentEnemy = currentWave.enemies[currentEnemyCount];

        if (currentEnemyCount == currentWave.enemies.Count)
        {
            lastSpawned = true;
        }
        else
        {
            currentEnemyCount++;
        }

        if (currentEnemy != null && lastSpawned == false)
        {
            GameObject newEnemy = Instantiate(currentEnemy, transform);
            newEnemy.transform.position = path.GetFirst();

            PathFollower follower = newEnemy.GetComponent<PathFollower>();
            follower.SetPath(path);
        }
    }


    IEnumerator WaveLoop() // Add advanced spawn logic later
    {
        if (active == false) { yield break; }

        yield return new WaitForSeconds(currentCooldown);

        if (lastSpawned == true && (transform.childCount - 1) == 0)
        {
            active = false;

            currentWaveCount++;
            SetNewWave();
        }

        if (active == true && lastSpawned == false) SpawnEnemy();

        StartCoroutine(WaveLoop());
    }


    public void RemoveEnemies(bool setInactive)
    {
        if  (setInactive == true) { active = false; }

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    void SetNewWave()
    {
        lastSpawned = false;
        currentEnemyCount = 0;

        currentWave = waves[currentWaveCount];
        currentCooldown = currentWave.cooldown;
    }
}
