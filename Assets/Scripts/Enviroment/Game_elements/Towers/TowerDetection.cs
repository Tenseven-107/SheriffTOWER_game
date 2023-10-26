using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    // Detects enemies

    [SerializeField] public float areaSize = 2.5f; // Size of the detection zone
    [SerializeField] Vector2 areaOrigin = Vector2.zero; // Location of the detection zone

    [SerializeField] int areaLayerSelf = 6; // Towers own physics layer
    [SerializeField] int areaLayer = 8; // Physics layer of the targets it needs to detect

    enum Ranges { CLOSEST, FURTHEST }; // Variable for getting the closest or furthest enemy in detection zone
    [SerializeField] Ranges range = Ranges.CLOSEST;

    [SerializeField] bool testing; // If the tower should show it's detection area

    public GameObject lockedEnemy; // Current focused enemy


    private void Start()
    {
        gameObject.layer = areaLayerSelf; // Sets gameObjects layer to the given layer

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>(); // Adds a second collider to tower for checking if the enemy left the detection area
        collider.isTrigger = true;
        collider.radius = areaSize;
    }


    private void Update()
    {
        CheckArea();
    }

    // Checks the Area for enemies
    void CheckArea()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(areaOrigin + (Vector2)transform.position, areaSize, areaLayer << (areaLayer / 2)); // Collider for checking enemies in detection zone

        // If there is no current locked enemy and there are enemies detected:
        if (hits.Length > 0 && lockedEnemy == null)
        {
            List<float> distances = new List<float>(); // Make new list of every enemy's distance

            // Add every enemy their distance to the list
            foreach(Collider2D hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                distances.Add(distance);
            }

            // Locks on the closest enemy or furthest enemy depending on it's range variable
            float selectedDistance;
            if (range == Ranges.CLOSEST)
            {
                selectedDistance = distances.Min();
            }
            else
            {
                selectedDistance = distances.Max();
            }
            SetLock(selectedDistance, hits);
        }
    }

    // Sets the current locked enemy to NULL when exiting the detection area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lockedEnemy)
        {
            lockedEnemy = null;
        }
    }

    // Check which enemy in the given list has the given distance
    void SetLock(float distance, Collider2D[] hits)
    {
        foreach(Collider2D hit in hits)
        {
            float hitDistance = Vector2.Distance(transform.position, hit.transform.position);
            if (hitDistance == distance)
            {
                lockedEnemy = hit.gameObject;
                break;
            }
        }
    }

    // Debug thingy
    private void OnDrawGizmos()
    {
        if (testing == true) Gizmos.DrawWireSphere(areaOrigin + (Vector2)transform.position, areaSize);
    }
}
