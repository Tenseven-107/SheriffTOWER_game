using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    MoneyBag moneyBag;


    private void Start()
    {
        moneyBag = GetComponent<MoneyBag>();
        text.enabled = false;
    }


    public void UpdateUI()
    {
        text.text = moneyBag.currentMoney.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            text.enabled = false;
        }
    }
}
