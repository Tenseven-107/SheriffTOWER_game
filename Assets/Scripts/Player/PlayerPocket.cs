using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPocket : MonoBehaviour
{
    // The storage of the current item the player is holding

    [SerializeField] public GameObject heldItem; // Current held item
    ItemData itemData; // Data of the currently held item

    [SerializeField] public bool itemCanUpgrade = false; // If the held item is able to upgrade towers

    [SerializeField] public GameObject weapon; // if the held item is a weapon
    [SerializeField] Transform weaponSlot; // The transform where the player weapon should be spawned in

    [SerializeField] SpriteRenderer itemSprite; // Sprite of the item
    PlayerMovement player; // Movement script of the player

    [SerializeField] Transform dropPos; // Location where items will be dropped
    PlacementMarker placementMarker; // Marker to show where towers will be placed

    [SerializeField] UnityEvent onAction; // Invoked when an item is dropped or picked up


    float bufferTime = 1f; // Cooldown for picking up and dropping items
    bool canDrop = false; // If the player is able to drop


    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        placementMarker = GetComponentInChildren<PlacementMarker>();
    }


    private void Update()
    {
        // Drops the item when key is pressed
        if (Input.GetKeyDown(KeyCode.E) && heldItem != null && canDrop == true)
        {
            DropItem();
        }

        // Shows a marker of where a tower will be placed if the currently held item is a tower
        if (heldItem != null && itemData.type == ItemData.ItemTypes.TOWER)
        {
            placementMarker.isActive = true;
            placementMarker.moveToPos(dropPos.position);
        }
        else{ placementMarker.isActive = false; }
    }

    // Equip a new item
    public void AddItem(GameObject newItem)
    {
        // Drops the currently held item if the player is currently holding an item
        if (heldItem != null)
        {
            DropItem();
        }

        onAction.Invoke();

        canDrop = false;
        StartCoroutine(PickupBuffer()); // Starts cooldown for dropping and picking up

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

    IEnumerator PickupBuffer()
    {
        yield return new WaitForSeconds(bufferTime);
        canDrop = true;
        yield break;
    }


    // Drops the currently held item
    void DropItem()
    {
        onAction.Invoke();

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
