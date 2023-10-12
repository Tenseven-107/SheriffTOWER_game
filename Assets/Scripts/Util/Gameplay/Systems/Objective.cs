using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    
    public int currentHP = 0;
    [SerializeField] int maxHP = 100;

    [SerializeField] Slider[] sliders;
    [SerializeField] Animation anim;

    [SerializeField] EnemySpawner spawner;
    [SerializeField] UnityEvent atDamage;


    private void Start()
    {
        currentHP = maxHP;

        foreach(Slider slider in sliders)
        {
            slider.minValue = -maxHP;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        
        foreach (Slider slider in sliders)
        {
            slider.value = -currentHP;
        }
        
        atDamage.Invoke();
        anim.Play();

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
