using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public static bool isBabyHungry;
    
    // Start is called before the first frame update
    void Start()
    {
        thePlayerManager = GetComponent<PlayerManager>();
        view = GetComponent<PhotonView>();
        currenthunger = hunger;
    }

    private void Update()
    {
        view.RPC("BabyStatusCount", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void BabyStatusCount()
    {
        //Hunger
        if (currenthunger > 0)
        {
            //when do you want to tell the player that the baby is hungry
            if (currenthunger == whenHungerTelling)
            {
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
            Debug.Log("Baby Hunger became 0");
        }
    }

    public void FullHunger()
    {
        currenthunger = hunger;
    }
}
