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

    //Event time counting
    [Header("Timer")]
    [SerializeField] private float timeDuration;
    private float timer;
    
    //Baby Status Checker
    public static bool isBabyHungry;
    public static bool isBabyCrying;
    public static bool isBabySleepy;
    public static bool isBabyWhining;
    public static bool isEventStart;


    //Baby Events Counter 
    [Header("How many Baby events?")]
    [Tooltip("Please add the number, if you have more events")]
    [SerializeField] private int babyEventsCount;

    
    void Start()
    {
        thePlayerManager = GetComponent<PlayerManager>();
        view = GetComponent<PhotonView>();
        currenthunger = hunger;
        babyStatusReset();
        TryResetEventTimer();
    }

    private void Update()
    {
        //view.RPC("BabyHungryStatusCount", RpcTarget.AllBuffered);
        if (PhotonNetwork.IsMasterClient)
        {
            Countdown();
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

    private void Countdown()
    {
        if (!isEventStart)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (19 < timer && timer < 20)
                {
                    Debug.Log(timer);
                }

                else if (10 < timer && timer < 11)
                {
                    Debug.Log(timer);
                }
            }

            else if(timer <= 0)
            {
                StartEvent();
            }  
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
            Debug.Log("Event start!");
            ChooseRandomEvent();
        }

        else
        {
            if(isBabyHungry)
                Debug.Log("Baby is already hungry");
            else if(isBabyCrying)
                Debug.Log("Baby is already crying");
        }
    }
    
    void ChooseRandomEvent()
    { 
        Debug.Log("Try Random num!");
        isBabyCrying = true;
        int _randomNum = Random.Range(0,babyEventsCount);
        view.RPC("StartRandomEvent", RpcTarget.AllBuffered, _randomNum);
    }
    
    [PunRPC]
    private void StartRandomEvent(int _randomNum)
    {
        if (_randomNum == 0)
        {
            if (!isBabyHungry && !isBabyWhining)
            {
                isBabySleepy = true;
                Debug.Log("Baby Sleepy!");
            }
        }
        
        else if (_randomNum == 1)
        {
            if (!isBabyHungry && !isBabySleepy)
            {
                Debug.Log("Baby whining!");
                isBabyWhining = true;
            }
        }
    }

    //Hunger Status Countdown
    [PunRPC]
    private void BabyHungryStatusCount()
    {
        //Hunger
        if (currenthunger > 0)
        {
            //when do you want to tell the player that the baby is hungry
            if (currenthunger == whenHungerTelling)
            {
                //stop baby event. Baby need to feed first
                view.RPC("ResetEventTimer", RpcTarget.All);
                isBabyHungry = true;
            }
            else if (whenHungerTelling < currenthunger)
            {
                isBabyHungry = false;
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
        Debug.Log("Full hunger");
    }
}
