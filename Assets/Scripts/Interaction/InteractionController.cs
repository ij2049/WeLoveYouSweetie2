using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public enum InteractableItems
{
    Bottle,
    Diaper,
    Vacuum
}

public enum InteractableFurniture
{
    Cradle,
    Bathtub,
    Trash,
    Bed,
    VacuumHolder,
    Door,
}

public class InteractionController : MonoBehaviourPunCallbacks
{
    [Header("Item")]
    [SerializeField] private bool isItem;
    [SerializeField] private InteractableItems interactableItems;
    
    [Space(10)]
    [Header("Furniture")]
    [SerializeField] private bool isFurniture;
    [SerializeField] private bool isHoldable;
    [SerializeField] private InteractableFurniture interactableFurniture;

    [Space(10)]
    [Header("UI")]
    [SerializeField] private GameObject obj_buttonInfo;
    [SerializeField] private TextMeshProUGUI txt_buttonInfo;

    
    private string itemsInfo;
    private PhotonView view;
    private GameObject player;
    private string furnitureInfo;
    private bool isPlayerEntered;
    private InteractionController theInteractionController;

    private void Awake()
    {
        theInteractionController = GetComponent<InteractionController>();
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        ItemInfo(); 
        FurnitureInFo();
    }

    private void Update()
    {
        if (theInteractionController.isPlayerEntered && view.IsMine && !FurnitureType.isPlayerWorking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteratcion(theInteractionController.player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            view = other.gameObject.GetComponent<PhotonView>();
            theInteractionController.player = other.gameObject;
            theInteractionController.isPlayerEntered = true;
            if (view.IsMine)
            {
                ShowBtnInfo();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        obj_buttonInfo.SetActive(false);
        theInteractionController.isPlayerEntered = false;
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
                case InteractableItems.Vacuum:
                    theInteractionController.itemsInfo = "Vacuum";
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
                case InteractableFurniture.Bed:
                    theInteractionController.furnitureInfo = "Bed";
                    break;
                case InteractableFurniture.VacuumHolder:
                    theInteractionController.furnitureInfo = "VacuumHolder";
                    break;
                case InteractableFurniture.Door:
                    theInteractionController.furnitureInfo = "Door";
                    break;
            }
        }
    }

    private void ShowBtnInfo()
    {
        //if interaction is for item
        if (theInteractionController.isItem && !theInteractionController.isFurniture)
        {
            txt_buttonInfo.text = "Press (E) to hold " + theInteractionController.itemsInfo;
            obj_buttonInfo.SetActive(true);
        }
        
        //if interaction is for furniture
        else if (theInteractionController.isFurniture && !theInteractionController.isItem)
        {
                        
            txt_buttonInfo.text = "Press (E) to use " + theInteractionController.furnitureInfo;
            obj_buttonInfo.SetActive(true);
        }
    }
    
    private void TryInteratcion(GameObject _player)
    {
        view = GetComponent<PhotonView>();

        //Items
        if (theInteractionController.isItem && !theInteractionController.isFurniture)
        {

            Debug.Log("Try Item Interaction!");

            if (!PlayerInventory.isItemHolding)
            {
                PlayerInventory.isItemHolding = true;
                obj_buttonInfo.SetActive(false);
                Debug.Log("Player is not holding item!");
                string _playerObjName = _player.name;
                view.RPC("HoldItem", RpcTarget.AllBuffered,_playerObjName);
            }

            else
            {
                Debug.Log("player is already holding the item");
            }
        }
        
        //Furniture
        else if(theInteractionController.isFurniture && !theInteractionController.isItem)
        {
            Debug.Log("Try furniture Interaction!");

            if (!theInteractionController.isHoldable)
            {
                if (!PlayerInventory.isItemHolding)
                {
                    obj_buttonInfo.SetActive(false);
                    PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                    UseFurniture(_playerInventory);
                }

                else
                {
                    //using cradle
                    if (BabyManager.isBabyHold || theInteractionController.furnitureInfo == "Cradle")
                    {
                        obj_buttonInfo.SetActive(false);
                        PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                        UseFurniture(_playerInventory);
                    }
                    
                    else if (PlayerInventory.isItemHolding && theInteractionController.furnitureInfo == "VacuumHolder")
                    {
                        obj_buttonInfo.SetActive(false);
                        PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                        UseFurniture(_playerInventory);
                    }
                    
                    else
                    {
                        Debug.Log("Sth is hold");
                    }
                }
            }
            else
            {
                if (PlayerInventory.isItemHolding)
                {
                    PlayerInventory _playerInventory = _player.gameObject.GetComponent<PlayerInventory>();
                    UseFurniture(_playerInventory);
                    obj_buttonInfo.SetActive(false);
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
    
    [PunRPC]
    void HoldItem(string _playerObjName)
    {
        GameObject _temp = GameObject.Find(_playerObjName);
        PlayerInventory _playerInventory = _temp.GetComponent<PlayerInventory>();

        for (int i = 0; i < _playerInventory.thePlayerInventory.playerItems.Length; i++)
        {
            //if the one of the player's items' name is samw as a interactable item, make item set active true
            if (theInteractionController.itemsInfo == _playerInventory.playerItems[i].itemsName)
            {
                //holding item
                _playerInventory.thePlayerInventory.playerItems[i].itemObject.SetActive(true);
                Debug.Log("Try Holditem");
                
                //check this is Bottle or not
                if (_playerInventory.thePlayerInventory.playerItems[i].itemsName == "Bottle")
                {
                    _playerInventory.thePlayerInventory.holdingItems.isThisPlayerBottleHold = true;
                }
                
                //check this is vacuum or not
                else if (_playerInventory.thePlayerInventory.playerItems[i].itemsName == "Vacuum")
                {
                    _playerInventory.thePlayerInventory.holdingItems.isThisPlayerVacuumHold = true;
                    ItemType.isSomePlayerHoldVacuum = true;
                    DirtManager theDirtManager = FindObjectOfType<DirtManager>();
                    theDirtManager.obj_vacuumHolder.SetActive(true);
                    gameObject.SetActive(false);
                }
                
                else
                {
                    Debug.Log("No list item is hold checking. Please check " + _playerInventory.gameObject.name + " this object's playerInventory");
                }
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
