using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPocket : MonoBehaviour
{
    [SerializeField] public GameObject heldItem;
    ItemData itemData;

    [SerializeField] public bool itemCanUpgrade = false;

    [SerializeField] public GameObject weapon;
    [SerializeField] Transform weaponSlot;

    [SerializeField] SpriteRenderer itemSprite;
    PlayerMovement player;

    [SerializeField] Transform dropPos;
    PlacementMarker placementMarker;


    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        placementMarker = GetComponentInChildren<PlacementMarker>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && heldItem != null)
        {
            DropItem();
        }

        if (heldItem != null && itemData.type == ItemData.ItemTypes.TOWER)
        {
            placementMarker.isActive = true;
            placementMarker.moveToPos(dropPos.position);
        }
        else{ placementMarker.isActive = false; }
    }


    public void AddItem(GameObject newItem)
    {
        if (heldItem != null)
        {
            DropItem();
        }

        itemData = newItem.GetComponent<ItemData>();
        heldItem = newItem;
        
        if (itemData.type == ItemData.ItemTypes.WEAPON)
        {
            weapon = Instantiate(itemData.weapon, weaponSlot);
            player.equip = PlayerMovement.EquipState.ARMED;
        }
        else
        {
            itemSprite.sprite = itemData.itemSprite;
            player.equip = PlayerMovement.EquipState.ITEM;
        }

        if (itemData.upgrader == true) { itemCanUpgrade = true; }
    }


    void DropItem()
    {
        heldItem.transform.position = dropPos.position;

        heldItem.SetActive(true);
        heldItem.GetComponent<BaseItem>().Drop();

        // Place tower if ItemType is TOWER
        if (itemData.type == ItemData.ItemTypes.WEAPON)
        {
            Destroy(weapon);
        }

        itemCanUpgrade = false;
        weapon = null;
        heldItem = null;
        itemData = null;
        itemSprite.sprite = null;

        player.equip = PlayerMovement.EquipState.UNARMED;
    }
}
