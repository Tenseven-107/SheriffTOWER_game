using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRanged : MonoBehaviour
{
    BulletShooter shooter;
    Transform shooterTrans;

    TowerDetection area;


    private void Start()
    {
        shooter = GetComponentInChildren<BulletShooter>();
        shooterTrans = shooter.transform;

        area = GetComponent<TowerDetection>();
    }


    private void FixedUpdate()
    {
        if (area.lockedEnemy != null)
        {
            Transform enemyTrans = area.lockedEnemy.transform;
            Vector2 aimPos = enemyTrans.position - transform.position;

            var angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
            shooterTrans.rotation = Quaternion.Euler(0, 0, angle);

            shooter.Fire();
        }
    }
}
