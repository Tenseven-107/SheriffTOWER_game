using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    MoneyBag moneyBag;
    Transform itemContainer;
    [SerializeField] Transform spawnPos;


    private void Start()
    {
        GameObject bagObject = GameObject.FindWithTag("MoneyBag");
        moneyBag = bagObject.GetComponent<MoneyBag>();

        GameObject itemCont = GameObject.FindWithTag("ItemContainer");
        itemContainer = itemCont.transform;
    }


    public void Buy(GameObject spawnedItem, int cost)
    {
        if (spawnedItem != null)
        {
            if (moneyBag.CheckIfCanRemove(cost) == true)
            {
                moneyBag.RemoveMoney(cost);
                Instantiate(spawnedItem, spawnPos.position, Quaternion.Euler(0, 0, 0), itemContainer);
            }
        }
    }
}
