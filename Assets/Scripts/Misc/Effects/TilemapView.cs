using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapView : MonoBehaviour
{
    // Small effect that changes the angle of a tilemap, used for a 3d effect

    Grid grid; // Tilemap grid
    [SerializeField] float startingPoint = -1.11f; // Starting angle
    [SerializeField] float endPoint = -0.02f; // Final angle

    [SerializeField] float time = 1.75f; // Time of effect

    void Start()
    {
        grid = GetComponent<Grid>(); // Getting the grid

        Vector2 endVector = new Vector2(endPoint, endPoint);
        grid.cellGap = new Vector2(endPoint, startingPoint);
        DOTween.To(() => grid.cellGap, x => grid.cellGap = x, (Vector3)endVector, time); // Moving from starting to endpoint
    }


}
