using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIEnabler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject UI;

    [Header("FX")]
    [SerializeField] TweenMover UIMover;
    [SerializeField] TweenScaler UIScaler;


    private void Start()
    {
        UI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            UI.SetActive(true);

            UIMover.StartTween();
            UIScaler.StartTween();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            UI.SetActive(false);
        }
    }
}
