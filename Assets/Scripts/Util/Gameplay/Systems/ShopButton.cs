using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{

    [SerializeField] Shop shop;

    [SerializeField] GameObject item;
    [SerializeField] int cost = 1;

    [SerializeField] bool disableOnPress = false;

    public void Press(bool remove)
    {
        shop.Buy(item, cost, remove, gameObject);
        if (disableOnPress == true) gameObject.SetActive(false);
    }
}
