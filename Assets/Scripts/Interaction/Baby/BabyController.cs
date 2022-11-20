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

public class BabyController : MonoBehaviour
{
    private BabyController theBabyController;
    private BabyManager theBabyManager;
    public SpriteRenderer feedingGauge;
    [SerializeField] private BabyAction[] theBabyAction;
    
    private PhotonView view;

    private void Awake()
    {
        theBabyManager = FindObjectOfType<BabyManager>();
        view = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theBabyController = GetComponent<BabyController>();
    }

    // Update is called once per frame
    void Update()
    {
        //view.RPC("FeedingEventChecker",RpcTarget.AllBuffered);
        view.RPC("SleepyEventChecker",RpcTarget.AllBuffered);
        view.RPC("SoothingEventChecker",RpcTarget.AllBuffered);
       

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

    [PunRPC]
    void SleepyEventChecker()
    {
        //Sleeping Event
        if (BabyStatus.isBabySleepy)
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Sleepy")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
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
        }
    }

    [PunRPC]
    void SoothingEventChecker()
    {
        //Soothing Event
        if (BabyStatus.isBabyWhining)
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Whining")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                }
            }
        }

        else
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Whining")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
        }
    }
}
