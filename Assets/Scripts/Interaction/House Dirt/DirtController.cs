using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DirtController : MonoBehaviour
{
    private DirtController theDirtController;
    private DirtManager theDirtManager;
    private PhotonView view;
 
    private string playerName;
    private PlayerInventory thePlayerInventory;
    private bool isPlayerEntered;
    private bool possibleToClean;
    private void Start()
    {
        theDirtController = GetComponent<DirtController>();
        theDirtManager = FindObjectOfType<DirtManager>();
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (possibleToClean)
        {
            if (Input.GetButtonDown("Jump"))
            { 
                Debug.Log("Pressed Space");
                //Try Vacuum
                if (theDirtController.thePlayerInventory.holdingItems.isThisPlayerVacuumHold && PlayerInventory.isItemHolding)
                {
                    view.RPC("TryClean", RpcTarget.All);
                }

                else
                {
                    Debug.Log("bool is wrong");
                    Debug.Log("isThisPlayerVacuumHold : " + theDirtController.thePlayerInventory.holdingItems.isThisPlayerVacuumHold);
                    Debug.Log("PlayerInventory.isItemHolding : " + PlayerInventory.isItemHolding);
                }
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
                theDirtController.thePlayerInventory = other.GetComponent<PlayerInventory>();
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
                view.RPC("ResetBool", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.transform.parent.name == theDirtController.playerName)
            {
                view.RPC("ResetBool", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void TryClean()
    {
        if (theDirtManager == null)
        {
            theDirtManager = FindObjectOfType<DirtManager>(); theDirtManager.countDirtObj--;
        }
        
        else
        {
            theDirtManager.countDirtObj--;
        }
        
        Debug.Log(theDirtManager.countDirtObj);
        gameObject.SetActive(false);
    }

    //reset all bool(not possible to interact with dirts)
    [PunRPC]
    void ResetBool()
    {
        theDirtController.playerName = null;
        theDirtController.isPlayerEntered = false;
        possibleToClean = false;
    }
}
