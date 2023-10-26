using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementMarker : MonoBehaviour
{
    public bool isActive {  
        get
        {
            return active;
        }

        set
        {
            active = value;
            
            if (active == false)
            {
                sprite.enabled = false;
            }
        }
    }
    bool active = false;

    SpriteRenderer sprite;
    PositionToGridpos grid;
    CheckGridPosition gridChecker;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        grid = GetComponent<PositionToGridpos>();

        GameObject CheckObject = GameObject.FindGameObjectWithTag("TowerContainer");
        gridChecker = CheckObject.GetComponent<CheckGridPosition>();

        sprite.enabled = active;
    }


    public void moveToPos(Vector2 newPos)
    {
        if (active == true)
        {
            Vector2 gridPos = grid.PositionToGrid(newPos);
            transform.position = gridPos;

            if (gridChecker.CheckIfAvailable(gridPos) == true)
            {
                sprite.enabled = true;
            }
            else { sprite.enabled = false; }
        }
    }
}
