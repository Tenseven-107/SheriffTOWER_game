using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    MoneyBag moneyBag;

    [Header("UI")]
    [SerializeField] Canvas canvas;
    [SerializeField] TextMeshProUGUI text;

    [Header("FX")]
    [SerializeField] TweenScaler backgroundScaler;
    [SerializeField] TweenMover backgroundMover;
    [SerializeField] TweenScaler bagScaler;


    private void Start()
    {
        moneyBag = GetComponent<MoneyBag>();
        canvas.enabled = false;
    }


    public void UpdateUI()
    {
        text.text = moneyBag.currentMoney.ToString();

        bagScaler.StartTween();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            canvas.enabled = true;

            backgroundScaler.StartTween();
            backgroundMover.StartTween();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
