using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject menu;
    RankCalculator calculator;

    [SerializeField] TextMeshProUGUI moneyScoreLabel;
    [SerializeField] TextMeshProUGUI waveScoreLabel;
    [SerializeField] TextMeshProUGUI totalScoreLabel;
    [SerializeField] TextMeshProUGUI rankLabel;


    private void Start()
    {
        calculator = GetComponent<RankCalculator>();
        menu.SetActive(false);
    }

    public void Activate()
    {
        menu.SetActive(true);

        calculator.CalculateRank();

        moneyScoreLabel.text = "Money left: " + calculator.moneyScore.ToString();
        waveScoreLabel.text = "Waves passed: " + calculator.waveScore.ToString();
        totalScoreLabel.text = "Total score: " + calculator.totalScore.ToString();

        rankLabel.text = calculator.currentRank;
        rankLabel.color = calculator.rankColor;
    }

    public void SwitchSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
