using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;

public enum Furniture
{
    Cradle,
    Bathtub,
    Trash,
    Bed
}

public class FurnitureType : MonoBehaviourPunCallbacks
{
    [SerializeField] private Furniture theFurniture;

    [Space(10)] [Header("Bed Info")] 
    [SerializeField] private Transform[] bedPos;
    
    private FurnitureType theFurnitureType;
    private string furnitureInfo;
    private BabyManager theBabyManager;
    private PhotonView view;
    private PlayerInventory thePlayerInventory;

    public static bool isPlayerUsingFurniture;

    private void Awake()
    {
        theFurnitureType = GetComponent<FurnitureType>();
        theBabyManager = FindObjectOfType<BabyManager>();
        isPlayerUsingFurniture = false;
    }

    private void Start()
    {
        theFurnitureType.view = GetComponent<PhotonView>();
    }

    public void TryFurniture(PlayerInventory _playerInvenotry)
    { 
        switch (theFurnitureType.theFurniture)
        {
            case Furniture.Cradle: TryCradle(_playerInvenotry); break;
            case Furniture.Bathtub: theFurnitureType.furnitureInfo = "Bathtub"; break;
            case Furniture.Trash: TryTrashbin(_playerInvenotry); break;
            case Furniture.Bed: StartCoroutine(TryBed(_playerInvenotry)); break;
        }
    }

    private void TryTrashbin(PlayerInventory _playerInventory)
    {
        Debug.Log("Try Trashbin!");

        if (PlayerInventory.isHolding)
        {
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName != "Baby")
                {
                    _playerInventory.playerItems[i].itemObject.SetActive(false);
                }
            }
            PlayerInventory.isHolding = false;
        }

        else
        {
            Debug.Log("Player is not holding");
        }
    }

    private void TryCradle(PlayerInventory _playerInventory)
    {
        theFurnitureType.thePlayerInventory = _playerInventory;
        Debug.Log(_playerInventory.gameObject.name);
        //if the baby is in the cradle and no one is holding baby -> Try baby hold from cradle
        if (BabyManager.isBabyCradle && !BabyManager.isBabyHold)
        {
            if (!_playerInventory.holdingItems.isThisPlayerBabyHold)
            {
                view.RPC("CurdleBoolSetting", RpcTarget.AllBuffered, false,true);
                _playerInventory.holdingItems.isThisPlayerBabyHold = true;
                for (int i = 0; i < _playerInventory.playerItems.Length; i++)
                {
                    if (_playerInventory.playerItems[i].itemsName == "Baby")
                    {
                        string _playerName;
                        _playerName = _playerInventory.gameObject.name;
                        view.RPC("EnablePlayerBaby", RpcTarget.AllBuffered,i,_playerName,true);

                            for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                        {
                            if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                            {
                                Debug.Log("Baby is Hold!");
                                view.RPC("CradleBabyOnOff", RpcTarget.AllBuffered, j,false);
                            }
                        }
                    }
                }
            }
        }
        //try to put baby insidethe cradle
        else if (!BabyManager.isBabyCradle && BabyManager.isBabyHold)
        {
            if (_playerInventory.holdingItems.isThisPlayerBabyHold)
            {
                view.RPC("CurdleBoolSetting", RpcTarget.AllBuffered, true,false);
                _playerInventory.holdingItems.isThisPlayerBabyHold = false;
                for (int i = 0; i < _playerInventory.playerItems.Length; i++)
                {
                    if (_playerInventory.playerItems[i].itemsName == "Baby")
                    {
                        string _playerName;
                        _playerName = _playerInventory.gameObject.name;
                        view.RPC("EnablePlayerBaby", RpcTarget.AllBuffered,i,_playerName,false);
                        
                        for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                        {
                            if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                            {
                                view.RPC("CradleBabyOnOff", RpcTarget.AllBuffered, j,true);
                            }
                        }
                        Debug.Log("Baby is now in the cradle!");
                    }
                }
            }
        }
    }

    [PunRPC]
    void CurdleBoolSetting(bool _isBabyCradle, bool isBabyHold)
    {
        BabyManager.isBabyCradle = _isBabyCradle;
        BabyManager.isBabyHold = isBabyHold;
    }

    [PunRPC]
    void EnablePlayerBaby(int _num, string _playerObjName, bool _isOn)
    {
        Debug.Log("player baby hold");
        GameObject _temp = GameObject.Find(_playerObjName);
        thePlayerInventory = _temp.GetComponent<PlayerInventory>();
        Debug.Log(thePlayerInventory.gameObject.name);
        thePlayerInventory.playerItems[_num].itemObject.SetActive(_isOn);
    }

    [PunRPC]
    void CradleBabyOnOff(int _cradleBabyNum, bool _isOn)
    {
        Debug.Log("remove baby from cradle");
        theFurnitureType.theBabyManager.theBabyInfo[_cradleBabyNum].obj_baby.SetActive(_isOn);
    }

    private IEnumerator TryBed(PlayerInventory _playerInventory)
    {
        PlayerStatusController _playerStatus = _playerInventory.gameObject.GetComponent<PlayerStatusController>();
        
        Debug.Log("TryBed");
        if (!isPlayerUsingFurniture)
        {
            isPlayerUsingFurniture = true;
            _playerInventory.gameObject.transform.position = bedPos[0].position;
            Debug.Log("Put player 0 position");

        }
        else
        {
            _playerInventory.gameObject.transform.position = bedPos[1].position;
            Debug.Log("Put player 1 position");
        }
        
        yield return new WaitForSeconds(4f);
        _playerStatus.FullSpStatus();
        _playerInventory.gameObject.transform.position = bedPos[2].position;
        Debug.Log("Now false");
        isPlayerUsingFurniture = false;
    }
}
