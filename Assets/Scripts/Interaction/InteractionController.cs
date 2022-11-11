using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public enum InteractableItems
{
    Bottle,
    Diaper
}

public enum InteractableFurniture
{
    Cradle,
    Bathtub
}

public class InteractionController : MonoBehaviour
{
    [Header("what kind of Interaction?")]
    //[Space(10)]
    [SerializeField] private bool isItem;
    [SerializeField] private InteractableItems interactableItems;
    [SerializeField] private bool isFurniture;
    [SerializeField] private InteractableFurniture interactableFurniture;


    private string itemsInfo;
    private string furnitureInfo;
    private InteractionController theInteractionController;

    private void Awake()
    {
        theInteractionController = GetComponent<InteractionController>();
    }

    private void Start()
    {
        ItemInfo(); 
        FurnitureInFo();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Sth entered!");
        if (other.CompareTag("Player"))
        {
            Debug.Log(theInteractionController.itemsInfo);
            TryInteratcion(other.gameObject);
        }

    }
    

    private void ItemInfo()
    {
        if (theInteractionController.isItem && !theInteractionController.isFurniture)
        {
            switch (theInteractionController.interactableItems)
            {
                case InteractableItems.Bottle:
                    theInteractionController.itemsInfo = "Bottle";
                    break;
                case InteractableItems.Diaper:
                    theInteractionController.itemsInfo = "Diaper";
                    break;
            }
        }
    }
    
    private void FurnitureInFo()
    {
        if (theInteractionController.isFurniture && !theInteractionController.isItem)
        {
            switch (theInteractionController.interactableFurniture)
            {
                case InteractableFurniture.Cradle:
                    theInteractionController.furnitureInfo = "Cradle";
                    break;
                case InteractableFurniture.Bathtub:
                    theInteractionController.furnitureInfo = "Bathtub";
                    break;
            }
        }
    }

    private void TryInteratcion(GameObject _player)
    {
        //Items
        if (theInteractionController.isItem && !theInteractionController.isFurniture)
        {
            Debug.Log("Try Item Interaction!");

            if (!PlayerController.isHolding)
            {
                PlayerController.isHolding = true;
                Debug.Log("Player is not holding item!");
                PlayerInventory _temp = _player.gameObject.GetComponent<PlayerInventory>();
                HoldItem(_temp);
            }
        }
        
        //Furniture
        else if(theInteractionController.isFurniture && !theInteractionController.isItem)
        {
            if (!PlayerController.isHolding)
            {
                UseFurniture();
            }

            else
            {
                Debug.Log("Player is holding item! Please make sure the hand is empty");
            }
        }

        else
        {
            Debug.Log("game object name : " + theInteractionController.gameObject.name + " have multiple choices for the interactions");
        }
    }

    private void HoldItem(PlayerInventory _playerInventory)
    {
        Debug.Log("Try Holditem");

        for (int i = 0; i < _playerInventory.playerItems.Length; i++)
        {
            //if the one of the player's items' name is samw as a interactable item, make item set active true
            if (theInteractionController.itemsInfo == _playerInventory.playerItems[i].itemsName)
            {
                _playerInventory.playerItems[i].itemObject.SetActive(true);
            }
        }
    }
    
    private void UseFurniture()
    {
        ;
    }
}
