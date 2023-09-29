using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField][Range(0.5f, 30)] float speed = 2;
    float moveDistance = 0.001f;
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
            float step = speed * Time.deltaTime;
            rb.position = Vector2.MoveTowards(rb.position, currentDestination, step);
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
