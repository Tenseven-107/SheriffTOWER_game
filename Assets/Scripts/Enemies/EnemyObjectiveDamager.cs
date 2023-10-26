using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectiveDamager : MonoBehaviour
{
    // Damages the objective when close to objective

    [SerializeField] int damage = 1; // Damage dealt to the objective
    float moveDistance; // Minimal distance between objective and enemy to deal damage

    Vector2 finalPos; // Final position of the enemy
    PathFollower follower; // The follower componnent of the enemy (makes the enemy follow the path)
    Path path; // The path the enemy follows

    Objective objective; // The final objective


    void Start()
    {
        // Getting the necessary componnents and variables
        follower = GetComponent<PathFollower>();

        moveDistance = follower.GetMoveDistance();

        path = follower.GetPath();
        finalPos = path.GetLast();

        GameObject objectiveObject = GameObject.FindWithTag("Objective"); // Gets the Objective GameObject
        objective = objectiveObject.GetComponent<Objective>(); // Sets the 'objective' reference to the Objective gameobject's Objective componnent
    }

    
    void Update()
    {
        // Checks if enemy is close enough to damage the objective, if so, damages the objective
        if (Vector2.Distance(transform.position, finalPos) < moveDistance)
        {
            objective.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
