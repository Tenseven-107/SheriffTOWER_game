using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    List<Vector3> points;
    LineRenderer line;


    private void Start()
    {
        line = GetComponent<LineRenderer>();

        Vector3[] pointArray = new Vector3[line.positionCount];
        line.GetPositions(pointArray);

        points = new List<Vector3>(pointArray);
        NormalizeZAxis();

        line.enabled = false;
    }

    void NormalizeZAxis()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 current = points[i];
            Vector3 newPos = new Vector3(current.x, current.y, 0);
            points[i] = newPos;
        }
    }


    public Vector2 GetFirst()
    {
        return points[0];
    }


    public Vector2 NextPoint(Vector2 currentPoint)
    {
        foreach (var point in points)
        {
            if ((Vector2)point == currentPoint)
            {
                int index = points.IndexOf(point) + 1;
                Vector2 nextPoint = points[index];

                return nextPoint;
            }
        }

        return Vector2.zero;
    }
}
