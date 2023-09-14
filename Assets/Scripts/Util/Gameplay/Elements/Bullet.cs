using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10; // Speed of the bullet
    public int damage = 1; // Damage of the bullet
    public int team = 0; // Bullets team

    public bool piercing = false; // Can pierce through entities
    public bool wallPiercing = false; // Can pierce through walls

    public Color color = Color.white; // Bullet color
    Rigidbody2D rb; // Rigidbody

    public GameObject fx; // Hit fx


    // Set up
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        GetComponent<SpriteRenderer>().material.color = color;
    }


    // Remove when out of screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    // When htting object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Entity>();

        if (collider)
        {
            if (collider.team != team)
            {
                collider.HandleHit(damage);

                spawnFX();
                if (!piercing) Destroy(gameObject);
            }
        }
        if (collision.tag == "Obstacle" && !wallPiercing)
        {
            spawnFX();
            Destroy(gameObject);
        }
    }


    // Spawn hit FX
    private void spawnFX()
    {
        if (fx != null)
        {
            Transform parent = transform.parent;
            Instantiate(fx, transform.position, Quaternion.Euler(0, 0, 0), parent);
        }  
    }
}
