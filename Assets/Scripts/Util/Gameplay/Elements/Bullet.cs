using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10; // Speed of the bullet
    [SerializeField] float timeAlive = 5;
    [SerializeField] int bulletLayer = 8;
    Rigidbody2D rb; // Rigidbody


    // Set up
    void Start()
    {
        gameObject.layer = bulletLayer; // set to bullet layer

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        StartCoroutine(AliveLoop());
    }


    // Remove when out of screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    // Time alive
    IEnumerator AliveLoop()
    {
        yield return new WaitForSeconds(timeAlive);
        Destroy(gameObject);
        yield break;
    }
}
