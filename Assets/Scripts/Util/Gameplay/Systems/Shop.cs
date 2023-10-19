using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{

    MoneyBag moneyBag;
    Transform itemContainer;
    [SerializeField] Transform spawnPos;

    [SerializeField] UnityEvent onBuy;
    [SerializeField] UnityEvent onNoBuy;


    private void Start()
    {
        GameObject bagObject = GameObject.FindWithTag("MoneyBag");
        moneyBag = bagObject.GetComponent<MoneyBag>();

        GameObject itemCont = GameObject.FindWithTag("ItemContainer");
        itemContainer = itemCont.transform;
    }


    public void Buy(GameObject spawnedItem, int cost, bool removeItem, GameObject item)
    {
        if (spawnedItem != null)
        {
            if (moneyBag.CheckIfCanRemove(cost) == true)
            {
                moneyBag.RemoveMoney(cost);
                Instantiate(spawnedItem, spawnPos.position, Quaternion.Euler(0, 0, 0), itemContainer);

                onBuy.Invoke();

                if (removeItem == true && item != null)
                {
                    item.SetActive(false);
                }
            }
            else { onNoBuy.Invoke(); }
        }
    }
}
