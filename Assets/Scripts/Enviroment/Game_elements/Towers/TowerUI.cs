using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    [SerializeField] TowerUpgrader upgrader;

    [SerializeField] GameObject UI;
    [SerializeField] GameObject UpgradeButton;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player") // Player.upgradekit == true
        {
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player") // Player.upgradekit == true
        {
            UI.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        if (upgrader.canUpgrade == false)
        {
            UpgradeButton.SetActive(false);
        }
    }
}
