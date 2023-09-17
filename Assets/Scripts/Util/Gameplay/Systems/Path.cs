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
    }


    public Vector2 GetFirst()
    {
        return points[0];
    }


    public Vector2 NextPoint(Vector2 currentPoint)
    {
        Vector2 nextPoint = Vector2.zero;

        foreach (var point in points)
        {
            if (points.Contains(point))
            {
                int index = points.IndexOf(point) + 1;
                nextPoint = points[index];

                return nextPoint;
            }
        }

        return nextPoint;
    }
}
