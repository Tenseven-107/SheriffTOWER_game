using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    [SerializeField] float areaSize = 1f;

    public enum Ranges { CLOSEST, FURTHEST };
    [SerializeField] public Ranges range = Ranges.CLOSEST;

    List<GameObject> enemies;
    public GameObject lockedEnemy;


    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

}
