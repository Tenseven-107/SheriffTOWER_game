using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    
    public int currentHP = 0;
    [SerializeField] int maxHP = 100;

    [SerializeField] Slider slider;
    [SerializeField] EnemySpawner spawner;


    private void Start()
    {
        currentHP = maxHP;
        slider.minValue = -maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        slider.value = -currentHP;

        if (currentHP <= 0)
        {
            GameOver();
        }
    }


    void GameOver()
    {
        spawner.RemoveEnemies(true);
    }
}
