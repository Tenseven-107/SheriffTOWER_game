using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy; // change to enemy list later
    [SerializeField] float spawnCooldown = 2.5f;

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
        yield return new WaitForSeconds(spawnCooldown);
        SpawnEnemy(enemy);
        StartCoroutine(SpawnLoop());
    }
}
