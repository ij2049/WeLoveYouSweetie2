using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rigidBody;
    
    private Vector2 movement;
    private PhotonView view;

    public bool isPlayerUsingNomoveFurniture;
    private PlayerController thePlayerController;
    private PlayerInventory thePlayerInventory;

    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
        thePlayerInventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        thePlayerController.view = GetComponent<PhotonView>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (thePlayerController.view.IsMine && !thePlayerController.isPlayerUsingNomoveFurniture)
        {
            PlayerMovement();
            if (thePlayerInventory.holdingItems.isThisPlayerBottleHold)
            {
                if (BabyManager.isBabyHold)
                {
                    Debug.Log("Try baby feeding!");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Try baby feeding! pressed E");
                        FeedingBaby();
                    }
                }
            }
        }
        
 
    }
    

    private void PlayerMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += input.normalized * moveSpeed * Time.deltaTime;
    }

    private void FeedingBaby()
    {
        ItemType _itemType;
        for (int i = 0; i < thePlayerInventory.playerItems.Length; i++)
        {
            if (thePlayerInventory.playerItems[i].itemsName == "Bottle")
            {
                _itemType = thePlayerInventory.playerItems[i].itemObject.GetComponent<ItemType>();
                if (_itemType != null)
                {
                    _itemType.TryFeedBaby(thePlayerInventory, i);
                }
            }
        }
    }
}
