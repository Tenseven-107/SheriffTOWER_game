using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy; // change to enemy list later
    [SerializeField] float spawnCooldown = 2.5f;
    bool active = true;

    [SerializeField] Path path;



    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }


    void SpawnEnemy(GameObject spawnEnemy)
    {
        if (spawnEnemy != null)
        {
            GameObject newEnemy = Instantiate(enemy, transform);
            newEnemy.transform.position = path.GetFirst();

            PathFollower follower = newEnemy.GetComponent<PathFollower>();
            follower.SetPath(path);
        }
    }


    IEnumerator SpawnLoop() // Add advanced spawn logic later
    {
        if (active == false) { yield break; }

        yield return new WaitForSeconds(spawnCooldown);
        if (active == true) SpawnEnemy(enemy);
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
