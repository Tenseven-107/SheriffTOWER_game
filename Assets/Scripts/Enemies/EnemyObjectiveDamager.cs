using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectiveDamager : MonoBehaviour
{
    [SerializeField] int damage = 1;
    float moveDistance;

    Vector2 finalPos;
    PathFollower follower;
    Path path;

    Objective objective;


    void Start()
    {
        follower = GetComponent<PathFollower>();

        moveDistance = follower.GetMoveDistance();

        path = follower.GetPath();
        finalPos = path.GetLast();

        GameObject objectiveObject = GameObject.FindWithTag("Objective");
        objective = objectiveObject.GetComponent<Objective>();
    }

    
    void Update()
    {
        if (Vector2.Distance(transform.position, finalPos) < moveDistance)
        {
            objective.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
