using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] float speed = 2;
    Vector2 velocity = Vector2.zero;
    Vector2 currentDestination = Vector2.zero;

    Rigidbody2D rb;
    Path path;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void FixedUpdate()
    {
        MoveLoop();
    }


    void MoveLoop()
    {
        if (rb.position != currentDestination)
        {
            Vector2 direction = currentDestination - rb.position;
            Vector2 normalizedVector = Vector3.Normalize(direction);

            velocity = normalizedVector * speed * Time.deltaTime;

            rb.MovePosition(rb.position + velocity);
        }
    }


    void SetPath(Path pathToFollow)
    {
        path = pathToFollow;
        currentDestination = path.GetFirst();

        transform.position = currentDestination;
    }
}
