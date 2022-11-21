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
    private BabyController theBabyController;
    private BabyManager theBabyManager;
    public SpriteRenderer feedingGauge;
    [SerializeField] private BabyAction[] theBabyAction;
    
    private PhotonView view;
    
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
        if (BabyStatus.isBabyCrying)
        {
            if (BabyStatus.isBabySleepy)
            {
                Debug.Log("Sleepy");
                view = GetComponent<PhotonView>();
                view.RPC("EventChecker", RpcTarget.All, "Sleepy");
            }

            if (BabyStatus.isBabyWhining)
            {
                Debug.Log("Whining");
                view = GetComponent<PhotonView>();
                view.RPC("EventChecker", RpcTarget.All, "Whining");
            }
        }
        
        else if (!BabyStatus.isBabySleepy || !BabyStatus.isBabyWhining)
        {
            if (BabyStatus.isBabyCrying)
                BabyStatus.isBabyCrying = false;
            else
            {
                if (!BabyStatus.isBabySleepy)
                {
                    EventTurnOff("Sleepy");
                }
                else if(!BabyStatus.isBabyWhining)
                {
                    EventTurnOff("Whining");
                }
            }
        }
        
        else
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Sleepy")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
            
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Whining")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
        }
    }

    private void EventTurnOff(string _eventName)
    {
        Debug.Log("Turning off events");
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
        Debug.Log("It's inside the Event Checker");
        for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
        {
            if (theBabyController.theBabyAction[i].actionName == _eventName)
            {
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
