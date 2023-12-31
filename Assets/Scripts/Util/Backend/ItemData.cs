using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] public Sprite itemSprite;

    public enum ItemTypes { ITEM, WEAPON, TOWER };
    [SerializeField] public ItemTypes type;

    [SerializeField] public bool upgrader = false;

    [SerializeField] public GameObject weapon;



    void Start()
    {
        if (itemSprite == null)
        {
            SpriteRenderer currentSprite = gameObject.GetComponent<SpriteRenderer>();
            itemSprite = currentSprite.sprite;
        }
    }
}
