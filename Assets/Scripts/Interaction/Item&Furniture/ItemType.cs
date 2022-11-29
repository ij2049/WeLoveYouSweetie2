using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum Item
{
    Bottle,
    Diaper,
    Vacuum
}
public class ItemType : MonoBehaviour
{

    [SerializeField] private Item theItem;
    private ItemType theItemType;
    private string itemInfo;

    private bool isBottleHold;
    private PhotonView view;

    private void Awake()
    {
        theItemType = GetComponent<ItemType>();
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {
        GetItemInfo();
    }

    private void GetItemInfo()
    {
        switch (theItemType.theItem)
        {
            case Item.Bottle: theItemType.itemInfo = "Bottle"; break;
            case Item.Diaper: theItemType.itemInfo = "Diaper"; break;
            case Item.Vacuum: theItemType.itemInfo = "Vacuum"; break;
        }
    }

    public void TryFeedBaby(PlayerInventory _playerInventory, int _num)
    {
        string _playerObjName = _playerInventory.gameObject.name;
        view.RPC("FeedBaby", RpcTarget.All,_playerObjName, _num);

    }

    [PunRPC]
    void FeedBaby(string _playerObjName, int _num)
    {
        Debug.Log("Final step of feeding baby");
        BabyStatus.isBabyHungry = false;
        //get player object
        GameObject _player = GameObject.Find(_playerObjName);
        //get player inventory
        PlayerInventory _playerInventory = _player.GetComponent<PlayerInventory>();
        _playerInventory.playerItems[_num].itemObject.SetActive(false);
        //This player is holding baby bool 
        _playerInventory.thePlayerInventory.holdingItems.isThisPlayerBottleHold = false;
        //Baby is now full
        BabyStatus babyStatus = FindObjectOfType<BabyStatus>();
        babyStatus.FullHunger();
        //try check baby status separately
        for (int i = 0; i < _playerInventory.playerItems.Length; i++)
        {
            if (_playerInventory.playerItems[i].itemsName == "Baby")
            {
                BabyController theBabyController = _playerInventory.playerItems[i].itemObject.GetComponent<BabyController>();
                theBabyController.TryCheckBabyStatus();
            }
        }
    }

    public void TryVacuum()
    {
        
    }

    [PunRPC]
    void HoldVacuum()
    {
        
    }
    
}
