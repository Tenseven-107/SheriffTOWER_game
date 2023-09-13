using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPocket : MonoBehaviour
{
    [SerializeField] public GameObject heldItem;
    ItemData itemData;

    [SerializeField] public GameObject weapon;
    [SerializeField] Transform weaponSlot;

    SpriteRenderer itemSprite;
    PlayerMovement player;


    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }


    public void Add_item(GameObject newItem)
    {
        if (heldItem != null)
        {
            DropItem();
        }

        itemData = newItem.GetComponent<ItemData>();
        heldItem = newItem;
        
        if (itemData.type == ItemData.ItemTypes.WEAPON)
        {
            weapon = itemData.weapon;
            weapon.transform.parent = weaponSlot;

            Instantiate(weapon);

            player.equip = PlayerMovement.EquipState.ARMED;
        }
        else
        {
            itemSprite.sprite = itemData.itemSprite;
            player.equip = PlayerMovement.EquipState.ITEM;
        }
    }


    void DropItem()
    {
        // Create instance of item in container on position

        // Place tower if ItemType is TOWER

        weapon = null;
        heldItem = null;
        itemData = null;
        itemSprite = null;

        player.equip = PlayerMovement.EquipState.UNARMED;
    }
}
