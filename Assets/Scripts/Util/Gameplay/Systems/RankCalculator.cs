using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCalculator : MonoBehaviour
{
    [Header("Ranks")]
    [SerializeField] List<ScoreRank> ranks = new List<ScoreRank>();

    [Header("Objects")]
    [SerializeField] MoneyBag moneyBag;
    [SerializeField] EnemySpawner enemySpawner;

    [Header("Score and Rank")]
    public int moneyScore = 0;
    public int waveScore = 0;
    public int totalScore = 0;

    public string currentRank = "F";
    public Color rankColor = Color.blue;


    public void CalculateRank()
    {
        moneyScore = moneyBag.currentMoney * 100;
        waveScore = enemySpawner.currentWaveCount * 100;

        totalScore = waveScore + moneyScore;

        for (int index = 0; index < ranks.Count; index++)
        {
            if (totalScore > ranks[index].scoreNeeded)
            {
                currentRank = ranks[index].rank;
                rankColor = ranks[index].color;
            }
        }
    }
}
