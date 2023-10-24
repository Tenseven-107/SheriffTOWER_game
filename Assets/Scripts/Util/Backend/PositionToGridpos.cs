using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToGridpos : MonoBehaviour
{
    [SerializeField] Vector2 gridSize = new Vector2(0.9f, 0.9f);

    public Vector2 PositionToGrid(Vector2 normalPosition)
    {
        float gridX = gridSize.x * 2;
        float gridY = gridSize.y * 2;

        float roundX = Mathf.Round(normalPosition.x / gridX);
        float roundY = Mathf.Round(normalPosition.y / gridY);

        Vector2 gridPosition = new Vector3(roundX * gridX, roundY * gridY);

        return gridPosition;
    }
}
