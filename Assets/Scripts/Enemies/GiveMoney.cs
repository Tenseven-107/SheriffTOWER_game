using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMoney : MonoBehaviour
{
    // Gives money when AddMoney is called

    [SerializeField] int money = 2; // Money added to moneybag
    [SerializeField] bool destroy = false; // if true, destroys gameobject when money is added

    MoneyBag moneyBag; // The moneybag object the added money is stored in

    private void Start()
    {
        GameObject bag = GameObject.FindGameObjectWithTag("MoneyBag"); // Gets moneybag gameobject
        moneyBag = bag.GetComponent<MoneyBag>(); // Sets moneybag reference to moneybag's 'MoneyBag' componnent
    }

    // Adds money when called
    public void AddMoney()
    {
        moneyBag.AddMoney(money); 
        
        if (destroy == true)
        {
            Destroy(gameObject);
        }
    }
}
