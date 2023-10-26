using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRaycastDetect : MonoBehaviour
{
    // Raycast instead of collider to detect enemies

    [SerializeField] public float range = 10; // Rnage of Raycast

    [SerializeField] int areaLayerSelf = 6; // Towers own physics layer
    [SerializeField] int areaLayer = 8; // Physics layer of enemies needing to be detected

    [SerializeField] bool testing = false; // Debug value for showing Raycast

    Vector2 direction = Vector2.zero; // The direction of the ray


    private void Start()
    {
        gameObject.layer = areaLayerSelf;

        SetDirection();
    }


    public bool Detect()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, areaLayer << (areaLayer / 2));
        if (hit.collider != null)
        {
            return true;
        }
        else { return false; }
    }

    private void OnDrawGizmos()
    {
        if (testing == true)
        {
            Gizmos.DrawLine(transform.position, direction);
            SetDirection();
        }
    }


    void SetDirection()
    {
        direction = new Vector2(transform.position.x, transform.position.y - range);
    }
}
