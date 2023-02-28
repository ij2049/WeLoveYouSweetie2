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
    private BabyManager theBabyManager;

    private bool soothingActivate;
    public bool isWorking;
    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
        thePlayerInventory = GetComponent<PlayerInventory>();
        theBabyManager = FindObjectOfType<BabyManager>();
    }

    private void Start()
    {
        thePlayerController.view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerController.view.IsMine && !thePlayerController.isPlayerUsingNomoveFurniture &&!isWorking && !GameManager.isGameOver)
        {
            PlayerMovement();
            
            //Try Bottle
            if (thePlayerInventory.holdingItems.isThisPlayerBottleHold && PlayerInventory.isItemHolding)
            {
                if (BabyManager.isBabyHold && BabyStatus.isBabyHungry)
                {
                    Debug.Log("Try baby feeding!");

                    if (Input.GetButtonDown("Jump"))
                    { 
                        view.RPC("TryFeeding", RpcTarget.All);   
                    }
                }
            }

            //Try Soothing
            if (thePlayerInventory.holdingItems.isThisPlayerBabyHold && BabyStatus.isBabyWhining &&
                BabyStatus.isBabyCrying && BabyManager.isBabyHold)
            {
                Debug.Log("Try to soothe baby!");
                
                if (Input.GetButtonDown("Jump"))
                { 
                    view.RPC("TrySoothing", RpcTarget.All);   
                }
            }

            else
            {
                if (BabyStatus.isBabyCrying)
                {
                    Debug.Log("some bool status is not working");
                }
            }
        }
    }

    private void PlayerMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += input.normalized * moveSpeed * Time.deltaTime;
    }

    [PunRPC]
    public void TryFeeding()
    {

        if (theBabyManager.feedingGauge >= 5)
        {
            if (!ItemType.isSomePlayerHoldVacuum)
            {
                PlayerInventory.isItemHolding = false;
            }
            thePlayerInventory.holdingItems.isThisPlayerBottleHold = false;
            theBabyManager.feedingGauge = 0.01f;
            FeedingBaby();
        }
        
        else
        {
            StartCoroutine(FeedingGaugeUpdate());
        }
    }
    
    private IEnumerator FeedingGaugeUpdate()
    {
        yield return new WaitForSeconds(0.5f);
        theBabyManager.feedingGauge++;
        Debug.Log("Try baby feeding! pressed space");
    }


    private void FeedingBaby()
    {
        theBabyManager.feedingGauge = 0.01f;
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

    //Soothing
    [PunRPC]
    void TrySoothing()
    {
        if (BabyStatus.isBabyWhining)
        {
            if (theBabyManager.soothingGauge >= 5)
            {
                StartCoroutine(SoothingComplete());
            }
        
            else
            {
                if (!soothingActivate)
                {
                    StartCoroutine(SoothingGaugeUpdate());
                }
            }
        }
    }
    
    IEnumerator SoothingGaugeUpdate()
    {
        soothingActivate = true;
        yield return new WaitForSeconds(0.3f);
        theBabyManager.soothingGauge++;
        soothingActivate = false;
        Debug.Log("Try baby soothing! pressed space");
    }
    
    IEnumerator SoothingComplete()
    {
        //soothing complete
        soothingActivate = false;
        BabyStatus.isBabyCrying = false;
        BabyStatus.isBabyWhining = false;
        BabyStatus theBabyStatus = FindObjectOfType<BabyStatus>();
        theBabyManager.soothingGauge = 0;
        theBabyStatus.TryResetEventTimer();
        BabyStatus.isHungryCountDone = false; //start feeding countdown
        yield return new WaitForSeconds(0.3f);
        BabyStatus.isEventStart = false;
        Debug.Log("Soothing Complet, isEventStart bool : " + BabyStatus.isEventStart);
    }
    
}
