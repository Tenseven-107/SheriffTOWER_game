using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] public Sprite itemSprite;

    public enum ItemTypes { ITEM, WEAPON, TOWER };
    [SerializeField] public ItemTypes type;

    [SerializeField] public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
