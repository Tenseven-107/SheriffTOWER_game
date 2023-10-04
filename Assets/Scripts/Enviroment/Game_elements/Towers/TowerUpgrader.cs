using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    [SerializeField] List<TowerUpgradeConstruct> towerUpgrades = new List<TowerUpgradeConstruct>();
    int nextUpgrade = 0;

    [SerializeField] MonoBehaviour towerMono;
    Type towerClass;



    private void Start()
    {
        towerClass = towerMono.GetType();
    }



    public void BuyUpgrade()
    {
        Upgrade();
    }


    void Upgrade()
    {
        TowerUpgradeConstruct upgrade = towerUpgrades[nextUpgrade];
        nextUpgrade++;

        switch (towerClass)
        {
            case Type type when type == typeof(TowerMelee): // Melee
                {
                    TowerMeleeDamager towerMelee = GetComponent<TowerMeleeDamager>();

                    towerMelee.cooldown -= upgrade.cooldownUpgrade;
                    towerMelee.damage += upgrade.damageUpgrade;

                    break;
                }

            case Type type when type == typeof(TowerRanged): // Ranger
                {
                    TowerDetection towerDetect = GetComponent<TowerDetection>();
                    BulletShooter shooter = GetComponentInChildren<BulletShooter>();

                    towerDetect.areaSize += upgrade.rangeUpgrade;

                    if (upgrade.newProjectile != null)
                    {
                        shooter.bullet = upgrade.newProjectile;
                    }

                    shooter.cooldown -= upgrade.cooldownUpgrade;
                    break;
                }

            case Type type when type == typeof(TowerPiercer): // Piercer
                {
                    TowerRaycastDetect towerDetect = GetComponent<TowerRaycastDetect>();
                    BulletShooter shooter = GetComponentInChildren<BulletShooter>();

                    towerDetect.range += upgrade.rangeUpgrade;

                    if (upgrade.newProjectile != null)
                    {
                        shooter.bullet = upgrade.newProjectile;
                    }

                    shooter.cooldown -= upgrade.cooldownUpgrade;
                    break;
                }

            case Type type when type == typeof(TowerBomber): // Bomber
                {
                    TowerDetection towerDetect = GetComponent<TowerDetection>();
                    TowerBomberShooter shooter = GetComponentInChildren<TowerBomberShooter>();

                    towerDetect.areaSize += upgrade.rangeUpgrade;

                    if (upgrade.newProjectile != null)
                    {
                        shooter.explosion = upgrade.newProjectile;
                    }

                    shooter.cooldown -= upgrade.cooldownUpgrade;
                    break;
                }
        }
    }
}
