using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Photon.Pun;

public class DirtManager : MonoBehaviour
{
    //count down
    [Header("Countdown")] 
    [SerializeField] private float timeDuration;
    private float timer;
    
    //dirts objects
    [Header("Dirts Objects")]
    [SerializeField] private GameObject[] obj_dirts;
    
    [Header("Vacuum Info")]
    public GameObject obj_vacuumHolder;
    
    [HideInInspector]
    public int countDirtObj;
    
    private GameObject tempGO;

    //data
    private PhotonView view;
    //bool
    private static bool isClean;
    
    private void Start()
    {
        isClean = true;
        view = GetComponent<PhotonView>();
        ResetTimer();
    }

    private void Update()
    {
        if (isClean && !FurnitureType.isPlayerWorking)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Countdown();
                //view.RPC("Countdown",RpcTarget.All);
                if(countDirtObj <= 0)
                {
                    view.RPC("CompleteClean",RpcTarget.All);
                }
            }
        }

        else
        {
            Debug.Log("IsClean false : " + isClean);
        }
    }

    //Countdown
    void Countdown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (13 < timer && timer < 14)
            {
                Debug.Log("dirt timer is working"!);
            }
        }

        else if(timer <= 0)
        {
            //view.RPC("TryShuffleDirts",RpcTarget.All);
            StartCoroutine(ShuffleDirts());
        }
    }

    private void ResetTimer()
    {
        countDirtObj = obj_dirts.Length;
        timer = timeDuration;
    }
    
    //shuffle dirts and turnOnObj;
    [PunRPC]
    void TryShuffleDirts()
    {
        StartCoroutine(ShuffleDirts());
    }

    IEnumerator ShuffleDirts()
    {
        isClean = false;
        countDirtObj = obj_dirts.Length;
        Debug.Log(countDirtObj);
        
        for (int i = 0; i < obj_dirts.Length; i++)
        {
            int rnd = Random.Range(0, obj_dirts.Length);
            view.RPC("AddShuffleObj", RpcTarget.All, i, rnd);
        }
        
        yield return new WaitForSeconds(1f);
        view.RPC("TryTurOnDirts", RpcTarget.All);
    }

    [PunRPC]
    void AddShuffleObj(int _num, int _rnd)
    {
        tempGO = obj_dirts[_rnd];
        obj_dirts[_rnd] = obj_dirts[_num];
        obj_dirts[_num] = tempGO;
    }

    [PunRPC]
    void TryTurOnDirts()
    {
        Debug.Log("start turn on");
        StartCoroutine(TurOnDirts());
    }
    
    IEnumerator TurOnDirts()
    {
        for (int i = 0; i < obj_dirts.Length; i++)
        {
            obj_dirts[i].SetActive(true);
            Debug.Log("dirt obj name : " + obj_dirts[i].name);
            yield return new WaitForSeconds(2f);
        }
    }
    
    [PunRPC]
    void CompleteClean()
    {
        Debug.Log("complete");
        ResetTimer();
        timer = timeDuration;
        isClean = true;
    }
    
}
