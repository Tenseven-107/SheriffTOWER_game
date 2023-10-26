using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMoney : MonoBehaviour
{
    bool active = false;

    [SerializeField] TextMeshProUGUI text;
    MoneyBag moneyBag;


    private void Start()
    {
        GameObject bag = GameObject.FindWithTag("MoneyBag");
        moneyBag = bag.GetComponent<MoneyBag>();
    }

    void Update()
    {
        if (active == true)
        {
            string moneyText = "Money: " + moneyBag.currentMoney.ToString();
            text.text = moneyText;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            active = false;
        }
    }
}
