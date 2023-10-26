using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveDamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] string hitTag = "Player";
    [SerializeField] string hitName = "PlayerHitbox";
    Objective objective;


    private void Start()
    {
        GameObject objectiveObject = GameObject.FindWithTag("Objective");
        objective = objectiveObject.GetComponent<Objective>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if (hitObject.tag == hitTag || hitObject.name == hitName)
        {
            objective.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
