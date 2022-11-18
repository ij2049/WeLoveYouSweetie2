using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum Item
{
    Bottle,
    Diaper
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
        }
    }

    public void TryFeedBaby(PlayerInventory _playerInventory, int _num)
    {
        string _playerObjName = _playerInventory.gameObject.name;
        PlayerInventory.isItemHolding = false;
        view.RPC("FeedBaby", RpcTarget.AllBuffered,_playerObjName, _num);

    }

    [PunRPC]
    void FeedBaby(string _playerObjName, int _num)
    {
        GameObject _player = GameObject.Find(_playerObjName);
        PlayerInventory _playerInventory = _player.GetComponent<PlayerInventory>();
        _playerInventory.playerItems[_num].itemObject.SetActive(false);
        _playerInventory.thePlayerInventory.holdingItems.isThisPlayerBottleHold = false;
        BabyStatus babyStatus = FindObjectOfType<BabyStatus>();
        babyStatus.FullHunger();
    }
    
}
