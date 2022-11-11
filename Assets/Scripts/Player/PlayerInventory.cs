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
    private PlayerInventory thePlayerInventory;

    private void Awake()
    {
        thePlayerInventory = GetComponent<PlayerInventory>();
    }
    
    
}
