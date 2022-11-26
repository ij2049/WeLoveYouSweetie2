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
        //view.RPC("FeedingEventChecker",RpcTarget.AllBuffered);
        
        if (!BabyStatus.isCountdownStart)
        {
            if (BabyStatus.isBabyCrying)
            {
                Debug.Log("isBabyCrying true");

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

        UpdateSpeechballoonStatus();
        
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
                Debug.Log("turning off events : " + theBabyController.theBabyAction[i].actionSpeechBubble.name);
                Debug.Log("the object name : " + theBabyController.gameObject.transform.parent.gameObject.transform.parent);
            }
        }

    }

    [PunRPC]
    void EventChecker(string _eventName)
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

    //this is local void for checking the status of speechballoon separately
    private void UpdateSpeechballoonStatus()
    {
        //Turn off the speechballoon
        if (!BabyStatus.isBabySleepy || !BabyStatus.isBabyWhining)
        {
            view = GetComponent<PhotonView>();

            if (!BabyStatus.isBabySleepy)
            {
                for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
                {
                    if (theBabyController.theBabyAction[i].actionName == "Sleepy")
                    {
                        theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                        Debug.Log("local sleepy turn off");

                    }
                }
            }
                
            if(!BabyStatus.isBabyWhining)
            {
                for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
                {
                    if (theBabyController.theBabyAction[i].actionName == "Whining")
                    {
                        theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                        Debug.Log("local Whining turn off");

                    }
                }
            }
        }
        //Turn on the speechballoon
        else if (BabyStatus.isBabySleepy || BabyStatus.isBabyWhining)
        {
            view = GetComponent<PhotonView>();

            if (BabyStatus.isBabySleepy)
            {
                for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
                {
                    if (theBabyController.theBabyAction[i].actionName == "Sleepy")
                    {
                        theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                        Debug.Log("local sleepy turn on");
                    }
                }
            }

            else if (BabyStatus.isBabyWhining)
            {
                for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
                {
                    if (theBabyController.theBabyAction[i].actionName == "Whining")
                    {
                        theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                        Debug.Log("local Whining turn on");

                    }
                }
            }
        }
    }

}
