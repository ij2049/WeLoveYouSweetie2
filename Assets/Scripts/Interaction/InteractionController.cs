using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableItems
{
    Bottle,
    Diaper
}

public class InteractionController : MonoBehaviour
{
    [Header("what kind of Interaction?")]
    //[Space(10)]
    [SerializeField] private bool isItem;
    [SerializeField] private bool isFurniture;

    private string itemsInfo;
    private InteractableItems interactableItems;
    private InteractionController theInteractionController;

    private void Awake()
    {
        theInteractionController = GetComponent<InteractionController>();
    }

    private void Start()
    {
        GetItemInfo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            TryInteratcion(other.gameObject);
        }
    }

    private void GetItemInfo()
    {
        switch (theInteractionController.interactableItems)
        {
            case InteractableItems.Bottle: itemsInfo = "Bottle"; break;
            case InteractableItems.Diaper: itemsInfo = "Diaper"; break;
        }
    }

    private void TryInteratcion(GameObject _player)
    {
        if (theInteractionController.isItem && !theInteractionController.isFurniture)
        {
            if (!PlayerController.isHolding)
            {
                PlayerInventory _temp = _player.gameObject.GetComponent<PlayerInventory>();
                HoldItem();
            }
        }
        
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

    private void HoldItem()
    {
        PlayerController.isHolding = true;

    }
    
    private void UseFurniture()
    {
        
    }
}
