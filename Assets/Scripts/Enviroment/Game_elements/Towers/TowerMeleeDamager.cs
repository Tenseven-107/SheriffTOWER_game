using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerMeleeDamager : MonoBehaviour
{
    [SerializeField] int damage = 1; // Damage value
    [SerializeField] int team = 0; // This damage dealers team

    [SerializeField] float cooldown = 1.5f;
    float last;

    [SerializeField] float areaSize = 2.5f;

    [SerializeField] int areaLayerSelf = 6;
    [SerializeField] int areaLayer = 8;

    [SerializeField] bool testing;

    public GameObject lockedEnemy;


    private void Start()
    {
        gameObject.layer = areaLayerSelf;
    }

    public void DamageArea()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, areaSize, areaLayer << (areaLayer / 2));

        if (hits.Length > 0 && lockedEnemy == null)
        {
            foreach (Collider2D hit in hits)
            {
                Entity hitEntity = hit.GetComponent<Entity>();
                if (hitEntity.team != team)
                {
                    hitEntity.HandleHit(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (testing == true)
        {
            Gizmos.DrawWireSphere(transform.position, areaSize);
        }
    }
}
