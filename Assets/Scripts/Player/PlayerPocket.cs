using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPocket : MonoBehaviour
{
    [SerializeField] public GameObject heldItem;
    ItemData itemData;

    [SerializeField] public GameObject weapon;
    [SerializeField] Transform weaponSlot;

    [SerializeField] SpriteRenderer itemSprite;
    PlayerMovement player;

    [SerializeField] Transform dropPos;


    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && heldItem != null)
        {
            DropItem();
        }
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

        weapon = null;
        heldItem = null;
        itemData = null;
        itemSprite.sprite = null;

        player.equip = PlayerMovement.EquipState.UNARMED;
    }
}
