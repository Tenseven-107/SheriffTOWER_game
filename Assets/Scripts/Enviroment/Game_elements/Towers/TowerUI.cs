using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField] TowerUpgrader upgrader;
    int cost = 0;

    [Header("UI")]
    [SerializeField] GameObject UI;
    [SerializeField] GameObject UpgradeButton;
    [SerializeField] TextMeshProUGUI costLabel;

    [Header("FX")]
    [SerializeField] Animation anims;
    [SerializeField] TweenScaler popupTween;



    private void Start()
    {
        cost = upgrader.GetCost();
        costLabel.text = "Cost: " + cost.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject colliderObject = collision.gameObject;

        if (colliderObject.tag == "Player") // Player.upgradekit == true
        {
            UI.SetActive(true);

            anims.Play();
            popupTween.StartTween();
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
