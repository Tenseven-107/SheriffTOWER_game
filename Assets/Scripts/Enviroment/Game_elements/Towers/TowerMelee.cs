using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMelee : MonoBehaviour
{
    // Logic for Melee towers to attack enemies within range

    TowerMeleeDamager damager;
    TowerDetection area;


    private void Start()
    {
        damager = GetComponentInChildren<TowerMeleeDamager>();
        area = GetComponent<TowerDetection>();
    }


    private void Update()
    {
        if (area.lockedEnemy != null)
        {
            damager.DamageArea();
        }
    }
}
