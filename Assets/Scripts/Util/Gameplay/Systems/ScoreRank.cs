using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreRank
{
    [SerializeField] public string rank = "F";
    [SerializeField] public Color color = Color.blue;
    [SerializeField] public int scoreNeeded = 0;
}
