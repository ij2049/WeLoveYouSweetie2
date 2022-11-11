using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Bottle,
    Diaper
}
public class ItemType : MonoBehaviour
{

    [SerializeField] private Item theItem;
    private ItemType theItemType;
    private string itemInfo;

    private void Awake()
    {
        theItemType = GetComponent<ItemType>();
    }

    private void Start()
    {
        CheckItem();
    }

    private void CheckItem()
    {
        switch (theItemType.theItem)
        {
            case Item.Bottle: theItemType.itemInfo = "Bottle"; break;
            case Item.Diaper: theItemType.itemInfo = "Diaper"; break;
        }
    }
}
