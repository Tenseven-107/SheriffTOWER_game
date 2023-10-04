using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUpgradeConstruct
{
    [Header("Default")]
    public int cost = 20;

    [Header("Universal Upgrades")]
    public float rangeUpgrade = 1f;
    public float cooldownUpgrade = 0.1f;
    public Sprite newSprite;

    [Header("Melee Upgrades")]
    public int damageUpgrade = 1;

    [Header("Ranged Upgrades")]
    public GameObject newProjectile = null;
}
