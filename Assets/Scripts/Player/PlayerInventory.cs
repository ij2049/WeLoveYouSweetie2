using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    public bool isThisPlayerVacuumHold;
}

public class PlayerInventory : MonoBehaviour
{
    //Please don't put data at the Unity
    [Header("Item Data")]
    public PlayerItems[] playerItems;
    public HoldingItems holdingItems;
    
    [Header("Inventory Data")]
    public PlayerInventory thePlayerInventory;

    public static bool isItemHolding;

    private void Awake()
    {
        thePlayerInventory = GetComponent<PlayerInventory>();
    }
}
