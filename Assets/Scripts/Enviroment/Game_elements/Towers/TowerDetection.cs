using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    [SerializeField] float areaSize = 2.5f;

    enum Ranges { CLOSEST, FURTHEST };
    [SerializeField] Ranges range = Ranges.CLOSEST;

    [SerializeField] bool testing;

    public GameObject lockedEnemy;


    private void Update()
    {
        CheckArea();
    }


    void CheckArea()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, areaSize);

        if (hits.Length > 0 )
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
        }
    }

    void SetLock(float distance)
    {

    }


    private void OnDrawGizmos()
    {
        if (testing == true) Gizmos.DrawWireSphere(transform.position, areaSize);
    }
}
