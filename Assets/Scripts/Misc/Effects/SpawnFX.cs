using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : MonoBehaviour
{
    // Spawns a new effect, which is a gameobject

    [SerializeField] Transform headTransform; // The transform of the object, needed to get the parent so that the effect can spawn in the same container as the object
    [SerializeField] GameObject fx; // Effect that wilol be spawned

    [SerializeField] bool giveRotation = false; // If spawned effect should inherit rotation
    [SerializeField] bool atStart = false; // If effect should be played at start


    private void Start()
    {
        if (atStart == true) { SpawnEffect(); }
    }

    // Spawns effect
    public void SpawnEffect()
    {
        Quaternion rot = Quaternion.Euler(Vector2.zero);
        if (giveRotation == true)
        {
            rot = transform.rotation;
        }

        Instantiate(fx, headTransform.position, rot, headTransform.parent);
    }
}
