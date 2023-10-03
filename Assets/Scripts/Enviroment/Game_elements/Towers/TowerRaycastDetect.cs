using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRaycastDetect : MonoBehaviour
{
    [SerializeField] public float range = 10;

    [SerializeField] int areaLayerSelf = 6;
    [SerializeField] int areaLayer = 8;

    [SerializeField] bool testing = false;

    Vector2 direction = Vector2.zero;


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
