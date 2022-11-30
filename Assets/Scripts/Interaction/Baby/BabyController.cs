using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[System.Serializable]
public class BabyAction
{
    public string actionName;
    public GameObject actionSpeechBubble;
}

public class BabyController : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    private BabyController theBabyController;
    private BabyManager theBabyManager;
    public SpriteRenderer feedingGauge;
    [SerializeField] private BabyAction[] theBabyAction;

    public static int whichStatusTurnedOff;
    private void Awake()
    {
        theBabyManager = FindObjectOfType<BabyManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theBabyController = GetComponent<BabyController>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        view.RPC("CheckBabyHunger", RpcTarget.All);

        if (!BabyStatus.isCountdownStart)
        {
            if (BabyStatus.isBabyCrying)
            {
                view = GetComponent<PhotonView>();

                if (BabyStatus.isBabySleepy)
                {
                    Debug.Log("Sleepy");
                    if (view != null)
                    {                
                        view.RPC("EventChecker", RpcTarget.All, "Sleepy");
                    }
                    else
                    {
                        Debug.Log("view is empty!");
                    }
                }

                if (BabyStatus.isBabyWhining)
                {
                    if (view != null)
                    {
                        view.RPC("EventChecker", RpcTarget.All, "Whining");
                        Debug.Log("Try whining turn on");

                    }
                    else
                    {
                        Debug.Log("view is empty!");
                    }
                }
            }

            else
            {
                Debug.Log("isBabyCrying false, isStatusTurnedOff true");

                if (!BabyStatus.isBabySleepy || !BabyStatus.isBabyWhining)
                {
                    view = GetComponent<PhotonView>();

                    if (!BabyStatus.isBabySleepy)
                    {
                        view.RPC("EventTurnOff", RpcTarget.All, "Sleepy");
                    }
                
                    if(!BabyStatus.isBabyWhining)
                    {
                        view.RPC("EventTurnOff", RpcTarget.All, "Whining");
                    }
                }
            }
        }

        else
        {
            Debug.Log("countdown start is true!");
        }

    }

    [PunRPC]
    void CheckBabyHunger()
    {
        theBabyController.feedingGauge.size = new Vector2(theBabyManager.feedingGauge,1);
        
        if (BabyStatus.isBabyHungry)
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
        }
    }

    [PunRPC]
    void EventTurnOff(string _eventName)
    {
        if (BabyStatus.isBabyCrying)
            BabyStatus.isBabyCrying = false;

        theBabyController = GetComponent<BabyController>();
        
        for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
        {
            if (theBabyController.theBabyAction[i].actionName == _eventName)
            {
                theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
            }
        }

    }

    [PunRPC]
    void EventChecker(string _eventName)
    {
        if (theBabyController != null)
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == _eventName)
                {
                    Debug.Log("turning on events : " + _eventName);

                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                }
            }
        }

        else
        {
            Debug.Log("ThebabyController null");
        }
        
    }

    public void TryCheckBabyStatus()
    {
        Debug.Log("TryCheckBabyStatus");
        view = GetComponent<PhotonView>();
        view.RPC("CheckBabyStatus", RpcTarget.All);
    }

    [PunRPC]
    void CheckBabyStatus()
    {
        if (BabyStatus.isBabyHungry)
        {
            theBabyController = GetComponent<BabyController>();
            theBabyController.feedingGauge.size = new Vector2(theBabyManager.feedingGauge,1);
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                }
            }
        }
        else
        {
            theBabyController = GetComponent<BabyController>();
            theBabyController.feedingGauge.size = new Vector2(theBabyManager.feedingGauge,1);
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
        }
        
        if (BabyStatus.isBabyCrying)
        {
            Debug.Log("isBabyCrying true");
            
            if (BabyStatus.isBabySleepy)
            {
                Debug.Log("Sleepy");
                if (view != null)
                {                
                    view.RPC("EventChecker", RpcTarget.All, "Sleepy");
                }
                else
                {
                    Debug.Log("view is empty!");
                }
            }

            if (BabyStatus.isBabyWhining)
            {
                Debug.Log("Whining");
                if (view != null)
                {
                    view.RPC("EventChecker", RpcTarget.All, "Whining");
                    Debug.Log("Try whining turn on");

                }
                else
                {
                    Debug.Log("view is empty!");
                }
            }
        }

        else
        {
            Debug.Log("isBabyCrying false, isStatusTurnedOff true");

            if (!BabyStatus.isBabySleepy || !BabyStatus.isBabyWhining)
            {
                if (!BabyStatus.isBabySleepy)
                {
                    view.RPC("EventTurnOff", RpcTarget.All, "Sleepy");
                }
                
                if(!BabyStatus.isBabyWhining)
                {
                    view.RPC("EventTurnOff", RpcTarget.All, "Whining");
                }
            }
        }
    }
    

}
