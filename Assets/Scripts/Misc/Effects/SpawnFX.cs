using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : MonoBehaviour
{
    [SerializeField] Transform headTransform;
    [SerializeField] GameObject fx;

    [SerializeField] bool giveRotation = false;
    [SerializeField] bool atStart = false;


    private void Start()
    {
        if (atStart == true) { SpawnEffect(); }
    }

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
