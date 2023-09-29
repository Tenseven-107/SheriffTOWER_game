using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyWave> waves = new List<EnemyWave>();
        
    float currentCooldown = 1f;
    public int currentWave = 0;
    GameObject currentEnemy;

    bool active = true;

    [SerializeField] Path path;



    private void Start()
    {
        currentWave = 0;

        StartCoroutine(SpawnLoop());
    }


    void SpawnEnemy(GameObject spawnEnemy)
    {
        if (spawnEnemy != null)
        {
            GameObject newEnemy = Instantiate(currentEnemy, transform);
            newEnemy.transform.position = path.GetFirst();

            PathFollower follower = newEnemy.GetComponent<PathFollower>();
            follower.SetPath(path);
        }
    }


    IEnumerator SpawnLoop() // Add advanced spawn logic later
    {
        if (active == false) { yield break; }

        yield return new WaitForSeconds(currentCooldown);
        if (active == true) SpawnEnemy(currentEnemy);
        StartCoroutine(SpawnLoop());
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
}
