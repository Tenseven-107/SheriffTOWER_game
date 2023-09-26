using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField][Range(0.5f, 30)] float speed = 2;
    float moveDistance = 0.5f;
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
        if (Vector2.Distance(rb.position, currentDestination) > moveDistance)
        {
            Vector2 direction = currentDestination - rb.position;
            Vector2 normalizedVector = Vector3.Normalize(direction);

            velocity = normalizedVector * speed * Time.deltaTime;

            rb.MovePosition(rb.position + velocity);
        }
        else
        {
            currentDestination = path.NextPoint(currentDestination);
        }
    }


    public void SetPath(Path pathToFollow)
    {
        path = pathToFollow;
        currentDestination = path.GetFirst();

        transform.position = currentDestination;
    }

    public Path GetPath()
    {
        return path;
    }

    public float GetMoveDistance()
    {
        return moveDistance;
    }
}
