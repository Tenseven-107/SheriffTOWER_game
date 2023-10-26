using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    MoneyUI moneyUI;

    int CURRENTMONEY
    {
        set
        {
            currentMoney = value;

            moneyUI.UpdateUI();
        }

        get
        {
            return currentMoney;
        }
    }
    public int currentMoney = 0;


    private void Start()
    {
        moneyUI = GetComponent<MoneyUI>();
        CURRENTMONEY = 0;
    }



    public void AddMoney(int money)
    {
        CURRENTMONEY += money;
    }

    public void RemoveMoney(int money)
    {
        CURRENTMONEY -= money;
    }

    public bool CheckIfCanRemove(int removedMoney)
    {
        if ((CURRENTMONEY - removedMoney) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
