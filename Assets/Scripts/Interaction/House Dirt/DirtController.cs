using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DirtController : MonoBehaviour
{
    private DirtController theDirtController;
    private PhotonView view;
 
    private string playerName;
    private bool isPlayerEntered;
    private bool possibleToClean;
    private void Start()
    {
        theDirtController = GetComponent<DirtController>();
        //view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (possibleToClean)
        {
            if (Input.GetButtonDown("Jump"))
            { 
                Debug.Log("Pressed Space");
                //view.RPC("TryClean", RpcTarget.All);   
                TryClean();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            theDirtController = GetComponent<DirtController>();

            if (!theDirtController.isPlayerEntered)
            {
                theDirtController.playerName = other.gameObject.transform.parent.name;
                theDirtController.isPlayerEntered = true;
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.parent.name == theDirtController.playerName)
            {
                //view = GetComponent<PhotonView>();
                Debug.Log(other.gameObject.transform.parent.name);
                possibleToClean = true;
            }
            else
            {
                ResetBool();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.parent.name == theDirtController.playerName)
            {
                ResetBool();
            }
        }
    }

    //[PunRPC]
    void TryClean()
    {
        gameObject.SetActive(false);
    }

    //reset all bool(not possible to interact with dirts)
    private void ResetBool()
    {
        theDirtController.playerName = null;
        theDirtController.isPlayerEntered = false;
        possibleToClean = false;
    }
}
