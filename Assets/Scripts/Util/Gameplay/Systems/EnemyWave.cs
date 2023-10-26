using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave

{
    public float cooldown = 1;
    public List<GameObject> enemies = new List<GameObject>();
}
