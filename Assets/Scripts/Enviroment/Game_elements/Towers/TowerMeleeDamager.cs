using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TowerMeleeDamager : MonoBehaviour
{
    // Damages all enemies in it's area

    [SerializeField] public int damage = 1; // Damage value
    [SerializeField] int team = 0; // This damage dealers team

    [SerializeField] public float cooldown = 1.5f; // Cooldown of attacking
    float last; // Last time of attacking

    [SerializeField] public float areaSize = 2.5f; // Size of attack area

    [SerializeField] int areaLayerSelf = 6; // Attack object's own physics layer
    [SerializeField] int areaLayer = 8; // Physics layer of the targets it needs to damage

    [SerializeField] bool testing; // When true, shows attack area

    [SerializeField] UnityEvent meleeEvent; // Invoked when attacking

    public GameObject lockedEnemy; // Current locked enemy


    private void Start()
    {
        gameObject.layer = areaLayerSelf; // Sets gameObjects layer to the given layer
    }

    public void DamageArea()
    {
        // Checks if cooldown period is over
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        if (meleeEvent != null) { meleeEvent.Invoke(); } // Invokes UnityEvent when attacking

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, areaSize, areaLayer << (areaLayer / 2)); // Creates damage zone

        // Damages every enemy in the zone if there is no currently locked enemy
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

    // Debug
    private void OnDrawGizmos()
    {
        if (testing == true)
        {
            Gizmos.DrawWireSphere(transform.position, areaSize);
        }
    }
}
