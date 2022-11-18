using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerItems
{
    public string itemsName;
    public GameObject itemObject;
}

[System.Serializable]
public class HoldingItems
{
    public bool isThisPlayerBabyHold;
    public bool isThisPlayerBottleHold;
}

public class PlayerInventory : MonoBehaviour
{ 
    public PlayerItems[] playerItems;
    public HoldingItems holdingItems;
    public PlayerInventory thePlayerInventory;
    
    public static bool isItemHolding;

    private void Awake()
    {
        thePlayerInventory = GetComponent<PlayerInventory>();
    }
}
