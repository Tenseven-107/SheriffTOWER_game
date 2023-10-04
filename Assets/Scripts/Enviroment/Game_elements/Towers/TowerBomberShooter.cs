using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerBomberShooter : MonoBehaviour
{
    [SerializeField] public float cooldown = 1f;
    float last;

    [SerializeField] public GameObject explosion;


    public void Bomb(Vector2 location)
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        Instantiate(explosion, location, Quaternion.Euler(Vector3.zero), transform);
    }
}
