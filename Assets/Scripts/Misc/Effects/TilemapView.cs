using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapView : MonoBehaviour
{
    Grid grid;
    [SerializeField] float startingPoint = -1.11f;
    [SerializeField] float endPoint = -0.02f;

    [SerializeField] float time = 1.75f;

    void Start()
    {
        grid = GetComponent<Grid>();

        Vector2 endVector = new Vector2(endPoint, endPoint);
        grid.cellGap = new Vector2(endPoint, startingPoint);
        DOTween.To(() => grid.cellGap, x => grid.cellGap = x, (Vector3)endVector, time);
    }


}
