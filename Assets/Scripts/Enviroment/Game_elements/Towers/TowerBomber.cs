using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBomber : MonoBehaviour
{
    // Logic for Bomb towers to fire at enemies within range

    TowerBomberShooter shooter;
    TowerDetection area;

    private void Start()
    {
        shooter = GetComponentInChildren<TowerBomberShooter>();
        area = GetComponent<TowerDetection>();
    }

    private void Update()
    {
        if (area.lockedEnemy != null)
        {
            Vector2 enemyLocation = area.lockedEnemy.transform.position;
            shooter.Bomb(enemyLocation);
        }
    }
}
