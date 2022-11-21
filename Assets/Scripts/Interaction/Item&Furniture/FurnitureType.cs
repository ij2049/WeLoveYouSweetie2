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
    public static bool isBedUsing;
    
    private void Awake()
    {
        theFurnitureType = GetComponent<FurnitureType>();
        theBabyManager = FindObjectOfType<BabyManager>();
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

        if (PlayerInventory.isItemHolding)
        {
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName != "Baby")
                {
                    string _playerName = _playerInventory.gameObject.name;
                    view.RPC("TrashbinEmptyHand", RpcTarget.All,_playerName,i);
                }
            }
            PlayerInventory.isItemHolding = false;
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
                view.RPC("CurdleBoolSetting", RpcTarget.All, false,true);
                _playerInventory.holdingItems.isThisPlayerBabyHold = true;
                for (int i = 0; i < _playerInventory.playerItems.Length; i++)
                {
                    if (_playerInventory.playerItems[i].itemsName == "Baby")
                    {
                        string _playerName = _playerInventory.gameObject.name;
                        view.RPC("EnablePlayerBaby", RpcTarget.All,i,_playerName,true);

                            for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                        {
                            if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                            {
                                Debug.Log("Baby is Hold!");
                                view.RPC("CradleBabyOnOff", RpcTarget.All, j,false);
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
                view.RPC("CurdleBoolSetting", RpcTarget.All, true,false);
                _playerInventory.holdingItems.isThisPlayerBabyHold = false;
                for (int i = 0; i < _playerInventory.playerItems.Length; i++)
                {
                    if (_playerInventory.playerItems[i].itemsName == "Baby")
                    {
                        string _playerName = _playerInventory.gameObject.name;
                        view.RPC("EnablePlayerBaby", RpcTarget.All,i,_playerName,false);
                        
                        for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                        {
                            if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                            {
                                view.RPC("CradleBabyOnOff", RpcTarget.All, j,true);
                                view.RPC("CheckBabySleepy", RpcTarget.All);
                            }
                        }
                        Debug.Log("Baby is now in the cradle!");
                    }
                }
            }
        }
    }
    
    private IEnumerator TryBed(PlayerInventory _playerInventory)
    {
        PlayerStatusController _playerStatus = _playerInventory.gameObject.GetComponent<PlayerStatusController>();
        string _playerName = _playerInventory.gameObject.name;
        PlayerController _thePlayerController = _playerInventory.gameObject.GetComponent<PlayerController>();

        Debug.Log("TryBed");
        if (!_thePlayerController.isPlayerUsingNomoveFurniture && !isBedUsing)
        {
            view.RPC("BedBoolSetting", RpcTarget.All,_playerName, true);
            view.RPC("BedPuttingPlayerPos", RpcTarget.All,_playerName ,0);
            Debug.Log("Put player 0 position");

        }
        else
        {
            view.RPC("BedPuttingPlayerPos", RpcTarget.All,_playerName ,1);

            Debug.Log("Put player 1 position");
        }
        
        yield return new WaitForSeconds(4f);
        _playerStatus.FullSpStatus();
        view.RPC("BedPuttingPlayerPos", RpcTarget.All,_playerName ,2);

        Debug.Log("Now false");
        view.RPC("BedBoolSetting", RpcTarget.All, _playerName,false);

    }
    
    [PunRPC]
    void BedPuttingPlayerPos(string _playerObjName, int _num)
    {
        GameObject _temp = GameObject.Find(_playerObjName);
        thePlayerInventory = _temp.GetComponent<PlayerInventory>();
        thePlayerInventory.gameObject.transform.position = bedPos[_num].position;
    }

    [PunRPC]
    void BedBoolSetting(string _playerObjName, bool _isUsing)
    {
        GameObject _temp = GameObject.Find(_playerObjName);
        PlayerController _playerController = _temp.GetComponent<PlayerController>();
        _playerController.isPlayerUsingNomoveFurniture = _isUsing;
        isBedUsing = _isUsing;
    }

    [PunRPC]
    void TrashbinEmptyHand(string _playerObjName, int _num)
    {
        GameObject _temp = GameObject.Find(_playerObjName);
        thePlayerInventory = _temp.GetComponent<PlayerInventory>();
        thePlayerInventory.playerItems[_num].itemObject.SetActive(false);
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

    //complete baby sleepy event
    [PunRPC]
    void CheckBabySleepy()
    {
        if (BabyStatus.isBabySleepy && BabyStatus.isBabyCrying)
        {
            BabyStatus _temp = FindObjectOfType<BabyStatus>();
            _temp.TryResetEventTimer();
            BabyStatus.isBabySleepy = false;
            BabyStatus.isBabyCrying = false;
            BabyStatus.isEventStart = false;
            Debug.Log("Baby is not sleepy anymore");
        }
        else
        {
            Debug.Log("Baby is not sleepy");
        }
    }
    
}
