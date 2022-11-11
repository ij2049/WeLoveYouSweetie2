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

public class PlayerInventory : MonoBehaviour
{ 
    public PlayerItems[] playerItems;
    public PlayerInventory thePlayerInventory;
    
    public static bool isHolding;

    private void Awake()
    {
        thePlayerInventory = GetComponent<PlayerInventory>();
    }
    
    
}
