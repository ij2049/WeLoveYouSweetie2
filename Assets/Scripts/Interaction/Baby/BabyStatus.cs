using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class BabyStatus : MonoBehaviour
{
    private PhotonView view;
    private PlayerManager thePlayerManager;

    [Header("Baby hunger")] 
    [SerializeField] private int hunger;
    private int currenthunger;
    [SerializeField] private int hungerDecreaseTime;
    [SerializeField] private int whenHungerTelling;
    private int currenthungerDecreaseTime;
    public static bool isHungryCountDone;

    //Event time counting
    [Header("Timer")]
    [SerializeField] private float timeDuration;
    private float timer;
    
    //Baby Status Checker
    public static bool isBabyHungry;
    public static bool isBabyCrying;
    public static bool isBabySleepy;
    public static bool isBabyWhining;

    //Baby Events Counter 
    [Header("How many Baby events?")]
    [Tooltip("Please add the number, if you have more events")]
    [SerializeField] private int babyEventsCount;

    public static bool isEventStart;
    public static bool isCountdownStart;
    
    
    void Start()
    {
        thePlayerManager = GetComponent<PlayerManager>();
        view = GetComponent<PhotonView>();
        currenthunger = hunger;
        babyStatusReset();
        TryResetEventTimer();
        if (!isCountdownStart)
        {
            view.RPC("CheckCountdownStart", RpcTarget.All, true);   
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !isEventStart)
        {
            //Hunger Event
            if (!isHungryCountDone)
            {
                view.RPC("BabyHungryStatusCount", RpcTarget.All);
            }

            if (isHungryCountDone) //after hungry countdown
            {
                //Random Event
                if (!isCountdownStart)
                {
                    view.RPC("CheckCountdownStart", RpcTarget.All, true);
                }
                Countdown();
            }
            else
            {
                Debug.Log("is not client or isEvent start true : " + isEventStart);
            }
        }
    }

    private void babyStatusReset()
    {
        isEventStart = false;
        isBabyHungry = false;
        isBabyCrying = false;
        isBabySleepy = false;
        isBabyWhining = false;
    }

    //Status Event Countdown
    public void TryResetEventTimer()
    {
        timer = timeDuration;
        isBabyCrying = false;
    }

    [PunRPC]
    void CheckCountdownStart(bool _onOff)
    {
        isCountdownStart = _onOff;
        Debug.Log("isCountdownStart is : " + isCountdownStart);
    }

    private void Countdown()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (13 < timer && timer < 14)
            {
                Debug.Log("timer is working"!);
            }
        }

        else if(timer <= 0)
        {
            StartEvent();
        }
    }

    private void StartEvent()
    {
        isEventStart = true;
        if (timer < 0)
        {
            timer = 0;
        }

        if (!isBabyHungry && !isBabyCrying && !isBabySleepy && !isBabyWhining)
        {
            ChooseRandomEvent();
        }

        else
        {
            if(isBabyHungry)
                Debug.LogError("Baby is already hungry");
            else if(isBabyCrying)
                Debug.LogError("Baby is already crying");
        }
    }
    
    void ChooseRandomEvent()
    { 
        int _randomNum = Random.Range(0,babyEventsCount);
        view.RPC("StartRandomEvent", RpcTarget.AllBuffered, _randomNum);
    }
    
    [PunRPC]
    private void StartRandomEvent(int _randomNum)
    {

        isBabyCrying = true;

        if (_randomNum == 0)
        {
            if (!isBabyHungry && !isBabyWhining && !isBabySleepy)
            {
                isBabySleepy = true;
                Debug.Log("Baby Sleepy!");
                isCountdownStart = false;
            }
        }
        
        else if (_randomNum == 1)
        {
            if (!isBabyHungry && !isBabySleepy && !isBabyWhining)
            {
                Debug.Log("Baby whining!");
                isBabyWhining = true;
                isCountdownStart = false;
            }
        }

        else
        {
            Debug.Log("is baby sleepy : " + isBabySleepy + ", is baby whining : " + isBabyWhining + ", is baby hungry : " + isBabyHungry);
        }
    }

    //Hunger Status Countdown
    [PunRPC]
    private void BabyHungryStatusCount()
    {
        Debug.Log("BabyHungerCountDownStart!");
        //Hunger
        if (currenthunger > 0)
        {
            //when do you want to tell the player that the baby is hungry
            if (currenthunger == whenHungerTelling)
            {
                isBabyHungry = true;
                Debug.Log("Baby is hungry now");
            }
            
            else if (whenHungerTelling < currenthunger)
            {
                isBabyHungry = false;
                Debug.Log("Baby is not hungry now");
            }
            
            if (currenthungerDecreaseTime <= hungerDecreaseTime)
            {
                currenthungerDecreaseTime++;
            }
            
            else
            {
                currenthunger--;
                currenthungerDecreaseTime = 0;
            }
        }
        else
        {
            //If hunger is 0
            currenthunger = 0;
            Debug.Log("Baby Hunger became 0");
        }
    }

    public void FullHunger()
    {
        currenthunger = hunger;
        Debug.Log("Full hunger : " + isHungryCountDone);
        isHungryCountDone = true;
    }
}
