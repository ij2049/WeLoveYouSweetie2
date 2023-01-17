using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public enum Furniture
{
    Cradle,
    Bathtub,
    Trash,
    Bed,
    VacuumHolder,
    Door,
    Work
}

[System.Serializable]
public class VacuumInfo
{
    public GameObject obj_vacuum;  
}

[System.Serializable]
public class WorkInfo
{
    public GameObject workPanel;
    public Transform playerWorkPos;
    public GameObject workingBG;
    public GameObject BGCollider;
    public GameObject BGFurniture;
    public GameObject working_UI;
}

public class FurnitureType : MonoBehaviourPunCallbacks
{
    [SerializeField] private Furniture theFurniture;

    [Space(10)] [Header("Bed Info")] 
    [SerializeField] private Transform[] bedPos;

    [Space(10)] [Header("Vacuum Info")] 
    [SerializeField] private VacuumInfo theVacuumInfo; //if the furniture is not a vacuum holder ignore it
    
    [Space(10)] [Header("Work Info")] 
    [SerializeField] private WorkInfo theWorkInfo; //if not door ignore it

    private FurnitureType theFurnitureType;
    private string furnitureInfo;
    private BabyManager theBabyManager;
    private GameManager theGameManager;
    private PhotonView view;
    private PlayerInventory thePlayerInventory;
    private PlayerInventory _tempPlayerInventory;
    public static bool isBedUsing;
    public static bool isPlayerWorking;

    private void Awake()
    {
        theGameManager = FindObjectOfType<GameManager>();
        theFurnitureType = GetComponent<FurnitureType>();
        theBabyManager = FindObjectOfType<BabyManager>();
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void TryFurniture(PlayerInventory _playerInvenotry)
    { 
        switch (theFurnitureType.theFurniture)
        {
            case Furniture.Cradle: TryCradle(_playerInvenotry); break;
            case Furniture.Bathtub: theFurnitureType.furnitureInfo = "Bathtub"; break;
            case Furniture.Trash: TryTrashbin(_playerInvenotry); break;
            case Furniture.Bed: StartCoroutine(TryBed(_playerInvenotry)); break;
            case Furniture.VacuumHolder: TryVacuumHolder(_playerInvenotry); break;
            case Furniture.Door: TryDoor(_playerInvenotry); break;
            case Furniture.Work: WorkPanelOn(_playerInvenotry); break;
        }
    }

    private void TryTrashbin(PlayerInventory _playerInventory)
    {
        Debug.Log("Try Trashbin!");

        if (PlayerInventory.isItemHolding)
        {
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName != "Baby" && _playerInventory.playerItems[i].itemsName != "Vacuum")
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
        Debug.Log(BabyManager.isBabyCradle);
        Debug.Log(BabyManager.isBabyHold);
        
        theFurnitureType.thePlayerInventory = _playerInventory;
        //if the baby is in the cradle and no one is holding baby -> Try baby hold from cradle
        if (BabyManager.isBabyCradle && !BabyManager.isBabyHold)
        {
            view = GetComponent<PhotonView>();
            if (!_playerInventory.holdingItems.isThisPlayerBabyHold)
            {
                Debug.Log("Baby is not holding by player");
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
            view = GetComponent<PhotonView>();
            
            if (_playerInventory.holdingItems.isThisPlayerBabyHold)
            {

                view.RPC("CurdleBoolSetting", RpcTarget.All, true, false);
                _playerInventory.holdingItems.isThisPlayerBabyHold = false;
                for (int i = 0; i < _playerInventory.playerItems.Length; i++)
                {
                    if (_playerInventory.playerItems[i].itemsName == "Baby")
                    {
                        string _playerName = _playerInventory.gameObject.name;
                        view.RPC("EnablePlayerBaby", RpcTarget.All, i, _playerName, false);

                        for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                        {
                            if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                            {
                                view.RPC("CradleBabyOnOff", RpcTarget.All, j, true);
                                view.RPC("TryCheckBabySleepy", RpcTarget.All);
                            }
                        }

                        Debug.Log("Baby is now in the cradle!");
                    }
                }
            }
        }

        else
        {
            Debug.Log("Cradle bool is wrong please check it");
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

    private void TryVacuumHolder(PlayerInventory _playerInventory)
    {
        if (PlayerInventory.isItemHolding)
        {
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName == "Vacuum")
                {
                    string _playerName = _playerInventory.gameObject.name;
                    Debug.Log("TryVacuumHolder");
                    view.RPC("TryPutBackVacuum", RpcTarget.All,_playerName, i);
                }
            }
        }
    }

    private void TryDoor(PlayerInventory _playerInventory)
    {
        //Try Door
    }

    private void WorkPanelOn(PlayerInventory _playerInventory)
    {
        theFurnitureType._tempPlayerInventory = _playerInventory;
        theFurnitureType.theWorkInfo.workPanel.SetActive(true);
    }
    
    //The working panel, pressed yes
    //Do you want to start work?
    public void WorkPanel_Yes()
    {
        PlayerStatusController _playerStatus = theFurnitureType._tempPlayerInventory.gameObject.GetComponent<PlayerStatusController>();
        string _playerName = theFurnitureType._tempPlayerInventory.gameObject.name;
        PlayerController _thePlayerController = theFurnitureType._tempPlayerInventory.gameObject.GetComponent<PlayerController>();
        if (!isPlayerWorking)
        {
            StartWorking();
            view.RPC("PlayerMoveToWork", RpcTarget.All,_playerName);
        }
    }

    //The working panel, pressed no
    public void WorkPanel_No()
    {
        theFurnitureType.theWorkInfo.workPanel.SetActive(false);
    }
    
    //player minigame start. This is not RPC. It's only happening on the playing player's view not the other player.
    private void StartWorking()
    {
        //starting bool for the minigame
        FurnitureType.isPlayerWorking = true;
        theFurnitureType.theGameManager.UI.SetActive(false);
        theFurnitureType.theWorkInfo.BGCollider.SetActive(false);
        theFurnitureType.theWorkInfo.BGFurniture.SetActive(false);
        theFurnitureType.theWorkInfo.workingBG.SetActive(true);
        theFurnitureType.theWorkInfo.working_UI.SetActive(true);
        //start robot assemble
    }
    
    //RPC Door
    [PunRPC]
    void PlayerMoveToWork(string _playerObjName)
    {
        Debug.Log("Rpc Door working");
        GameObject _tempPlayer = GameObject.Find(_playerObjName);
        PlayerController _playerController = _tempPlayer.GetComponent<PlayerController>();
        Debug.Log(_tempPlayer.gameObject.name);

        if (_playerController == null)
        {
            Debug.Log("_player Controller empty");
        }
        _tempPlayer.transform.position = theFurnitureType.theWorkInfo.playerWorkPos.position;
        _playerController.isWorking = true;
    }
    
    //RPC Bed
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

    //RPC Trashbin
    [PunRPC]
    void TrashbinEmptyHand(string _playerObjName, int _num)
    {
        GameObject _temp = GameObject.Find(_playerObjName);
        thePlayerInventory = _temp.GetComponent<PlayerInventory>();
        thePlayerInventory.playerItems[_num].itemObject.SetActive(false);
    }
    
    //RPC Curdle
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
        thePlayerInventory.holdingItems.isThisPlayerBabyHold = _isOn;
        thePlayerInventory.playerItems[_num].itemObject.SetActive(_isOn);
        
        //check baby status first for baby speechballoon turn On and Off
        BabyController theBabyController = thePlayerInventory.playerItems[_num].itemObject.gameObject.transform.GetComponent<BabyController>();
        if(theBabyController != null)
            theBabyController.TryCheckBabyStatus();
        else
            Debug.Log("theBabyController is null");
    }

    [PunRPC]
    void CradleBabyOnOff(int _cradleBabyNum, bool _isOn)
    {
        Debug.Log("baby cradle on or off : " + _isOn);
        theFurnitureType.theBabyManager.theBabyInfo[_cradleBabyNum].obj_baby.SetActive(_isOn);
        
        //check baby status first for baby speechballoon turn On and Off
        BabyController theBabyController =theFurnitureType.theBabyManager.theBabyInfo[_cradleBabyNum].obj_baby.GetComponent<BabyController>();
        if(theBabyController != null)
            theBabyController.TryCheckBabyStatus();
        else
            Debug.Log("theBabyController is null");
    }

    //RPC Vacuum Holder
    [PunRPC]
    void TryPutBackVacuum(string _playerObjName, int _num)
    {
        StartCoroutine(PutBackVacuum(_playerObjName, _num));
    }

    IEnumerator PutBackVacuum(string _playerObjName, int _num)
    {
        Debug.Log("Put Back Vacuum");
        //get player info
        GameObject _player = GameObject.Find(_playerObjName);
        thePlayerInventory = _player.GetComponent<PlayerInventory>();
        //turn on and off objs
        thePlayerInventory.playerItems[_num].itemObject.SetActive(false);
        theFurnitureType.theVacuumInfo.obj_vacuum.SetActive(true);
        //bool status
        PlayerInventory.isItemHolding = false;
        thePlayerInventory.thePlayerInventory.holdingItems.isThisPlayerVacuumHold = false;
        ItemType.isSomePlayerHoldVacuum = false;
        //turn off this trigger
        gameObject.SetActive(false);
        yield return null;
    }
    
    //complete baby sleepy event
    [PunRPC]
    void TryCheckBabySleepy()
    {
        StartCoroutine(SleepyComplete());
    }

    IEnumerator SleepyComplete()
    {
        if (BabyStatus.isBabySleepy && BabyStatus.isBabyCrying)
        {
            BabyStatus _temp = FindObjectOfType<BabyStatus>();
            _temp.TryResetEventTimer();
            BabyStatus.isBabySleepy = false;
            BabyStatus.isBabyCrying = false;
            BabyStatus.isHungryCountDone = false; //start feeding countdown
            Debug.Log("Baby sleeping complete : " + BabyStatus.isBabySleepy);
            yield return new WaitForSeconds(0.3f);
            BabyStatus.isEventStart = false;
        }
        else
        {
            Debug.Log("Baby is not sleepy");
            Debug.Log("BabyStatus.isBabySleepy : " + BabyStatus.isBabySleepy);
            Debug.Log("BabyStatus.isBabyCrying : " + BabyStatus.isBabyCrying);
        }
    }
    
    
}
