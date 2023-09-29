using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIEnabler : MonoBehaviour
{
    [SerializeField] GameObject UI;


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
