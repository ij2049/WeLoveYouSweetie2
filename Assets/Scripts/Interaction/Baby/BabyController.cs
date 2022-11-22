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
    
    public static bool isStatusTurnedOff;
    
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
        //view.RPC("FeedingEventChecker",RpcTarget.AllBuffered);
        if (BabyStatus.isBabyCrying && !isStatusTurnedOff)
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
            if (!BabyStatus.isBabySleepy || !BabyStatus.isBabyWhining)
            {
                if (isStatusTurnedOff)
                {
                    view = GetComponent<PhotonView>();

                    if (!BabyStatus.isBabySleepy)
                    {
                        Debug.Log("Try sleeping turn off");
                        view.RPC("EventTurnOff", RpcTarget.All, "Sleepy");
                    }
                    
                    if(!BabyStatus.isBabyWhining)
                    {
                        Debug.Log("Try whining turn off");
                        view.RPC("EventTurnOff", RpcTarget.All, "Whining");
                    }
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
        Debug.Log("Turning off events");
        for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
        {
            Debug.Log("turning off events : " + _eventName);
            if (theBabyController.theBabyAction[i].actionName == _eventName)
            {
                theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
            }
        }
    }

    [PunRPC]
    void EventChecker(string _eventName)
    {
        Debug.Log("It's inside the Event Checker");
        for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
        {
            if (theBabyController.theBabyAction[i].actionName == _eventName)
            {
                Debug.Log("turning on events : " + _eventName);

                theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
            }
        }
    }

    [PunRPC]
    void FeedingEventChecker()
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
    

}
