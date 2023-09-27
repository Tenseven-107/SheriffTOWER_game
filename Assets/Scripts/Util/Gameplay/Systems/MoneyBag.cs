using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public int currentMoney = 0;


    public void AddMoney(int money)
    {
        currentMoney += money;
    }

    public void RemoveMoney(int money)
    {
        currentMoney -= money;
    }

    public bool CheckIfCanRemove(int removedMoney)
    {
        if ((currentMoney - removedMoney) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
