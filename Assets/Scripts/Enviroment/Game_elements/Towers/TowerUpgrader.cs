using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class TowerUpgrader : MonoBehaviour
{
    [SerializeField] List<TowerUpgradeConstruct> towerUpgrades = new List<TowerUpgradeConstruct>();
    public int nextUpgrade = 0;

    [SerializeField] MonoBehaviour towerMono;
    Type towerClass;

    MoneyBag moneyBag;



    private void Start()
    {
        towerClass = towerMono.GetType();

        GameObject bagObject = GameObject.FindWithTag("MoneyBag");
        moneyBag = bagObject.GetComponent<MoneyBag>();
    }



    public void BuyUpgrade()
    {
        int cost = towerUpgrades[nextUpgrade].cost;

        if (moneyBag.CheckIfCanRemove(cost) == true && nextUpgrade <= towerUpgrades.Count)
        {
            moneyBag.RemoveMoney(cost);
            Upgrade();
        }
    }


    void Upgrade()
    {
        TowerUpgradeConstruct upgrade = towerUpgrades[nextUpgrade];
        nextUpgrade++;

        if (upgrade.newSprite != null)
        {
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = upgrade.newSprite;
        }

        switch (towerClass)
        {
            case Type type when type == typeof(TowerMelee): // Melee
                {
                    TowerMeleeDamager towerMelee = GetComponentInChildren<TowerMeleeDamager>();

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
