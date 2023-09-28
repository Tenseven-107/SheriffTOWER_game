using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{

    [SerializeField] Shop shop;

    [SerializeField] GameObject item;
    [SerializeField] int cost = 1;

    public void Press()
    {
        shop.Buy(item, cost);
    }
}
