using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPiercer : MonoBehaviour
{
    BulletShooter shooter;
    TowerRaycastDetect detector;


    private void Start()
    {
        shooter = GetComponentInChildren<BulletShooter>();
        detector = GetComponent<TowerRaycastDetect>();

        shooter.transform.rotation = Quaternion.Euler(0, 0, -90);
    }


    private void Update()
    {
        if (detector.Detect() == true)
        {
            shooter.Fire();
        }
    }
}
