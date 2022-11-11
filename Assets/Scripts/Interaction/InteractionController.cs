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
    Bathtub,
    Trash
}

public class InteractionController : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private bool isItem;
    [SerializeField] private InteractableItems interactableItems;
    
    [Space(10)]
    [Header("Furniture")]
    [SerializeField] private bool isFurniture;
    [SerializeField] private bool isHoldable;
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
                case InteractableFurniture.Trash:
                    theInteractionController.furnitureInfo = "Trash";
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

            if (!PlayerInventory.isHolding)
            {
                PlayerInventory.isHolding = true;
                Debug.Log("Player is not holding item!");
                PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                HoldItem(_playerInventory);
            }
        }
        
        //Furniture
        else if(theInteractionController.isFurniture && !theInteractionController.isItem)
        {
            Debug.Log("Try furniture Interaction!");

            if (!theInteractionController.isHoldable)
            {
                if (!PlayerInventory.isHolding)
                {
                    PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                    UseFurniture(_playerInventory);
                }

                else
                {
                    Debug.Log("Sth is hold");
                }
            }

            else
            {
                if (PlayerInventory.isHolding)
                {
                    PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                    UseFurniture(_playerInventory);
                }
                else
                {
                    Debug.Log("To use trashbin player need to hold sth");
                }

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

        for (int i = 0; i < _playerInventory.thePlayerInventory.playerItems.Length; i++)
        {
            //if the one of the player's items' name is samw as a interactable item, make item set active true
            if (theInteractionController.itemsInfo == _playerInventory.playerItems[i].itemsName)
            {
                _playerInventory.thePlayerInventory.playerItems[i].itemObject.SetActive(true);
            }
        }
    }
    
    private void UseFurniture(PlayerInventory _playerInventory)
    {
        Debug.Log("Use furniture!");
        FurnitureType _furniture = GetComponent<FurnitureType>();
        _furniture.TryFurniture(_playerInventory);
    }
}
