using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUpgradeConstruct
{
    // Constructor for upgrades

    [Header("Default")]
    public int cost = 20; // Cost of upgrade

    [Header("Universal Upgrades")]
    public float rangeUpgrade = 1f; // Upgrades range
    public float cooldownUpgrade = 0.1f; // Decrease of tower cooldown
    public Sprite newSprite; // New tower sprite

    [Header("Melee Upgrades")]
    public int damageUpgrade = 1; // Damage upgrade for Melee towers

    [Header("Ranged Upgrades")]
    public GameObject newProjectile = null; // New projectile fired by any ranged tower (Piercer, Bomber, Ranger)
}
