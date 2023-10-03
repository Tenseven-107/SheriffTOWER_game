using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    [SerializeField] public float areaSize = 2.5f;
    [SerializeField] Vector2 areaOrigin = Vector2.zero;

    [SerializeField] int areaLayerSelf = 6;
    [SerializeField] int areaLayer = 8;

    enum Ranges { CLOSEST, FURTHEST };
    [SerializeField] Ranges range = Ranges.CLOSEST;

    [SerializeField] bool testing;

    public GameObject lockedEnemy;


    private void Start()
    {
        gameObject.layer = areaLayerSelf;

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = areaSize;
    }


    private void Update()
    {
        CheckArea();
    }


    void CheckArea()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(areaOrigin + (Vector2)transform.position, areaSize, areaLayer << (areaLayer / 2));

        if (hits.Length > 0 && lockedEnemy == null)
        {
            List<float> distances = new List<float>();

            foreach(Collider2D hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                distances.Add(distance);
            }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lockedEnemy)
        {
            lockedEnemy = null;
        }
    }

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


    private void OnDrawGizmos()
    {
        if (testing == true) Gizmos.DrawWireSphere(areaOrigin + (Vector2)transform.position, areaSize);
    }
}
