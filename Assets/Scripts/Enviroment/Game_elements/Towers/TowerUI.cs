using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    // UI of the tower for upgrading and destroying towers

    [Header("Tower")]
    [SerializeField] TowerUpgrader upgrader; // Tower upgrader for upgrading towers
    int cost = 0; // Cost of next upgrade

    [Header("UI")]
    [SerializeField] GameObject UI; // UI element
    [SerializeField] GameObject UpgradeButton; // Button to upgrade
    [SerializeField] TextMeshProUGUI costLabel; // Label showing upgrade cost

    [Header("FX")]
    [SerializeField] Animation anims; // Animations played
    [SerializeField] TweenScaler popupTween; // Tween playing for the UI element appearing



    private void Start()
    {
        cost = upgrader.GetCost(); // Getting cost of the first upgrade
        costLabel.text = "Cost: " + cost.ToString(); // Sets label text to the cost
    }


    // Plays animation when player is close to tower with an Upgrade Kit item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            PlayerPocket playerPocket = colliderObject.GetComponent<PlayerPocket>();
            if (playerPocket.itemCanUpgrade == true)
            {
                UI.SetActive(true);

                anims.Play();
                popupTween.StartTween();
            }
        }
    }

    // Deactivates UI when player leaves
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player")
        {
            UI.SetActive(false);
        }
    }

    // Updates UI when interacted with
    public void UpdateUI()
    {
        if (upgrader.canUpgrade == true)
        {
            cost = upgrader.GetCost();
            costLabel.text = "Cost: " + cost.ToString();
        }
        else
        {
            costLabel.text = "MAX";
        }
        

        if (upgrader.canUpgrade == false)
        {
            UpgradeButton.SetActive(false);
        }
    }
}
