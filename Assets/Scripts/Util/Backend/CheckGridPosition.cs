using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGridPosition : MonoBehaviour
{
    public bool CheckIfAvailable(Vector2 checkPosition)
    {
        List<Vector2> takenPositions = new List<Vector2>();
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            takenPositions.Add(transform.GetChild(i).transform.position);
        }

        foreach (Vector2 position in takenPositions)
        {
            if (position == checkPosition)
            {
                return false;
            }
        }

        return true;
    }
}
