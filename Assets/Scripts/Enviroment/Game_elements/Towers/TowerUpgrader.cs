using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TowerUpgrader : MonoBehaviour
{
    // Upgrades towers when BuyUpgrade() is called

    [SerializeField] List<TowerUpgradeConstruct> towerUpgrades = new List<TowerUpgradeConstruct>(); // List of all the upgrades for this tower
    public int nextUpgrade = 0; // Number of the next upgrade
    public bool canUpgrade = true; // If tower can be upgraded

    // Class of the tower
    [SerializeField] MonoBehaviour towerMono;
    Type towerClass;

    SpriteRenderer sprite; // Sprite of the tower

    MoneyBag moneyBag; // Moneybag with all the players money

    [SerializeField] UnityEvent onUpgrade; // Invoked when upgraded
    [SerializeField] UnityEvent onNoUpgrade; // Called when not enough money for upgrade



    private void Start()
    {
        towerClass = towerMono.GetType(); // Sets the class reference of the tower to the type of the tower

        canUpgrade = true;

        GameObject bagObject = GameObject.FindWithTag("MoneyBag"); // Getting the moneybag gameobject
        moneyBag = bagObject.GetComponent<MoneyBag>(); // Setting the moneybag reference to the moneybag gameobject's moneybag componnent

        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    // Buy upgrade
    public void BuyUpgrade()
    {
        if (canUpgrade == true) // Checks if the tower can be upgraded
        {
            int cost = towerUpgrades[nextUpgrade].cost; // Cost of this upgrade

            // Check if money can be removed from moneybag, if so, upgrades tower
            if (moneyBag.CheckIfCanRemove(cost) == true)
            {
                moneyBag.RemoveMoney(cost);
                Upgrade();

                onUpgrade.Invoke();
            }
            else { onNoUpgrade.Invoke(); }
        }
    }

    // Upgrades stats of the tower depending on its class
    void Upgrade()
    {
        TowerUpgradeConstruct upgrade = towerUpgrades[nextUpgrade]; // Gets the current upgrade used to upgrade the tower
        nextUpgrade++;

        if (nextUpgrade >= towerUpgrades.Count)
        {
            canUpgrade = false;
        }

        if (upgrade.newSprite != null)
        {
            sprite.sprite = upgrade.newSprite;
        }

        switch (towerClass)
        {
            case Type type when type == typeof(TowerMelee): // Melee
                {
                    TowerDetection towerDetect = GetComponentInChildren<TowerDetection>();
                    TowerMeleeDamager towerMelee = GetComponentInChildren<TowerMeleeDamager>();

                    towerMelee.cooldown -= upgrade.cooldownUpgrade;
                    towerMelee.damage += upgrade.damageUpgrade;

                    towerDetect.areaSize += upgrade.rangeUpgrade;
                    towerMelee.areaSize += upgrade.rangeUpgrade;

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

    // Gets cost of the next upgrade
    public int GetCost()
    {
        int cost = towerUpgrades[nextUpgrade].cost;
        return cost;
    }
}
