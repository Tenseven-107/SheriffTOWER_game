using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TowerBomberShooter : MonoBehaviour
{
    // Fires bombs

    [SerializeField] public float cooldown = 1f; // Cooldown between shots
    float last; // Time of last shot

    [SerializeField] public GameObject explosion; // The explosion at the shot location

    [SerializeField] UnityEvent atFire; // Invoked when firing


    public void Bomb(Vector2 location)
    {
        // Checks if cooldown period is over
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        Instantiate(explosion, location, Quaternion.Euler(Vector3.zero), transform); // Spawns explosion on the given location
        atFire.Invoke(); // Invokes event when fired
    }
}
