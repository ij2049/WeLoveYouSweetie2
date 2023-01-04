using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAssembleManager : MonoBehaviour
{
    [SerializeField] private GameObject catalogPanel;
    [SerializeField] private GameObject btn_parts;
    [SerializeField] private GameObject obj_partsImg;
    private RobotPartsManager theRobotPartsManager;
    public static bool isCatalogOpened;

    private void Awake()
    {
        theRobotPartsManager = FindObjectOfType<RobotPartsManager>();
        FurnitureType.isPlayerWorking = true; //delete this later it's for test
    }

    private void Update()
    {
        if (FurnitureType.isPlayerWorking)
        {
            if (!isCatalogOpened)
            { 
                if (Input.GetKeyDown(KeyCode.E)) 
                { 
                    Debug.Log("Right");

                theRobotPartsManager.countPartsStack++;

                if (theRobotPartsManager.countPartsStack < 5 && 0 < theRobotPartsManager.countPartsStack)
                {
                    theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    Debug.Log(theRobotPartsManager.countPartsStack);
                }
                
                else
                {
                    Debug.Log("else");

                    if (4 < theRobotPartsManager.countPartsStack)
                    {
                        theRobotPartsManager.countPartsStack = 1;
                        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    }

                    else
                    {
                        theRobotPartsManager.countPartsStack = 4;
                        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    }
                }

            } 
                if (Input.GetKeyDown(KeyCode.Q))
            {
                theRobotPartsManager.countPartsStack--;
                Debug.Log("Left");
                if (theRobotPartsManager.countPartsStack < 5 && 0< theRobotPartsManager.countPartsStack)
                {
                    theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    Debug.Log(theRobotPartsManager.countPartsStack);
                }
                
                else
                {
                    Debug.Log("else");

                    if (4 < theRobotPartsManager.countPartsStack)
                    {
                        theRobotPartsManager.countPartsStack = 1;
                        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    }

                    else
                    {
                        theRobotPartsManager.countPartsStack = 4;
                        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                    }
                }
            } 
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ;
            } 
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                ;
            }
            }
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log("TryTab");

                if (!isCatalogOpened)
                {
                    catalogPanel.SetActive(true);
                    isCatalogOpened = true;
                    btn_parts.SetActive(false);
                    obj_partsImg.SetActive(false);
                }
                else
                {
                    catalogPanel.SetActive(false);
                    isCatalogOpened = false;
                    btn_parts.SetActive(true);
                    obj_partsImg.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("isPlayerWorking false");

        }
    }
}
