using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : MonoBehaviour
{
    [SerializeField] Transform headTransform;
    [SerializeField] GameObject fx;

    public void SpawnEffect()
    {
        Instantiate(fx, headTransform.position, Quaternion.Euler(Vector2.zero), headTransform.parent);
    }
}
