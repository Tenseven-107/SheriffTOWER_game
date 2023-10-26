using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    // Componnent that makes enemies follow their set path

    [SerializeField][Range(0.5f, 30)] float speed = 2; // Speed of how fast to follow the path
    float moveDistance = 0.001f; // Minimal distance needed from last point to stop
    Vector2 currentDestination = Vector2.zero; // The next point of the path

    Rigidbody2D rb; // Enemies RigidBody componnent
    Path path; // The path the enemy follows


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
        // Enemy moves towards next point in path until the distance is lower then the move distance, then sets the currentDestination to the next point
        if (Vector2.Distance(rb.position, currentDestination) > moveDistance)
        {
            float step = speed * Time.deltaTime;
            rb.position = Vector2.MoveTowards(rb.position, currentDestination, step);
        }
        else
        {
            currentDestination = path.NextPoint(currentDestination);
        }
    }

    // Sets the path of the enemy to the given path, gets the first point and sets to enemy's location to said point
    public void SetPath(Path pathToFollow)
    {
        path = pathToFollow;
        currentDestination = path.GetFirst();

        transform.position = currentDestination;
    }

    // returns path
    public Path GetPath()
    {
        return path;
    }

    // returns moveDistance
    public float GetMoveDistance()
    {
        return moveDistance;
    }
}
