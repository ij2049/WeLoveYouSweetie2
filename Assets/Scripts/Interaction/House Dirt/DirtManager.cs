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
    private GameObject tempGO;
    //data
    private PhotonView view;
    //bool
    private static bool isClean;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        ResetTimer();
    }

    private void Update()
    {
        if (!isClean)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Countdown();
            }
        }
    }

    //Countdown
    private void Countdown()
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
            view.RPC("TryShuffleDirts",RpcTarget.All);
        }
    }

    private void ResetTimer()
    {
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
        for (int i = 0; i < obj_dirts.Length; i++)
        {
            int rnd = Random.Range(0, obj_dirts.Length);
            tempGO = obj_dirts[rnd];
            obj_dirts[rnd] = obj_dirts[i];
            obj_dirts[i] = tempGO;
        }
        
        for (int i = 0; i < obj_dirts.Length; i++)
        {
            obj_dirts[i].SetActive(true);
            Debug.Log("dirt obj name : " + obj_dirts[i].name);
            yield return new WaitForSeconds(2f);
        }
    }

    public void CompleteClean()
    {
        ResetTimer();
        timer = timeDuration;
        isClean = true;
    }
    
}
