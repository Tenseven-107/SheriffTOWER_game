using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMoney : MonoBehaviour
{
    [SerializeField] int money = 2;
    [SerializeField] bool destroy = false;

    MoneyBag moneyBag;

    private void Start()
    {
        GameObject bag = GameObject.FindGameObjectWithTag("MoneyBag");
        moneyBag = bag.GetComponent<MoneyBag>();
    }

    public void AddMoney()
    {
        moneyBag.AddMoney(money);

        if (destroy == true)
        {
            Destroy(gameObject);
        }
    }
}
