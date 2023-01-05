using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private GameObject catalogPanel;
    [SerializeField] private GameObject btn_parts;
    [SerializeField] private GameObject obj_partsImg;
    
    private RobotPartsManager theRobotPartsManager;
    private SelectedPartsManager theSelectedPartsManager;
    private PartsSelectCursorController thePartsSelectCursorController;
    public List<int> selectedPartsNum = new List<int>();
    
    public static bool isCatalogOpened;

    private void Awake()
    {
        theRobotPartsManager = FindObjectOfType<RobotPartsManager>();
        thePartsSelectCursorController = FindObjectOfType<PartsSelectCursorController>();
        theSelectedPartsManager = FindObjectOfType<SelectedPartsManager>();
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
                    StartCoroutine(CheckSlot());
                    SelectedCursorReset();
                    
                    Debug.Log("Right");

                    //Stack counting
                    theRobotPartsManager.countPartsStack++;
                    CheckStacksForPartsCount();
                    Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);

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
                            thePartsSelectCursorController.slectedPartsInfoNum = 0;
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
                    StartCoroutine(CheckSlot());

                    Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);
                    SelectedCursorReset();

                    //Stack counting
                    theRobotPartsManager.countPartsStack--;
                    CheckStacksForPartsCount();

                    Debug.Log("Left"); 
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
                            thePartsSelectCursorController.slectedPartsInfoNum = 27;
                            theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
                            
                        } 
                    } 
                } 
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    thePartsSelectCursorController.countSelection++;
                    thePartsSelectCursorController.slectedPartsInfoNum++;//count which parts is cursor located
                    
                    Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);
                    
                    //count which parts are selected
                    if (27 < thePartsSelectCursorController.slectedPartsInfoNum)
                    {
                        thePartsSelectCursorController.slectedPartsInfoNum = 0;
                    }

                    // count for 1~7 for the selction position
                    if (thePartsSelectCursorController.countSelection < 7)
                    {
                        thePartsSelectCursorController.selection_rectTransform.anchoredPosition =
                            thePartsSelectCursorController.selection_Pos[thePartsSelectCursorController.countSelection];
                    }

                    else
                    {
                        thePartsSelectCursorController.countSelection = 0;
                        thePartsSelectCursorController.slectedPartsInfoNum = thePartsSelectCursorController.slectedPartsInfoNum - 7;
                        Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);

                        thePartsSelectCursorController.selection_rectTransform.anchoredPosition =
                            thePartsSelectCursorController.selection_Pos[thePartsSelectCursorController.countSelection];
                    }

                } 
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) 
                { 
                    thePartsSelectCursorController.countSelection--;
                    thePartsSelectCursorController.slectedPartsInfoNum--;//count which parts is cursor located
                    Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);

                    //count which parts are selected
                    if (thePartsSelectCursorController.slectedPartsInfoNum < 0)
                    {
                        thePartsSelectCursorController.slectedPartsInfoNum = 27;
                    }
                    
                    // count for 1~7 for the selction position
                    if (0 <= thePartsSelectCursorController.countSelection)
                    {
                        thePartsSelectCursorController.selection_rectTransform.anchoredPosition =
                            thePartsSelectCursorController.selection_Pos[thePartsSelectCursorController.countSelection];
                    }
                    
                    else
                    {
                        thePartsSelectCursorController.countSelection = 6;
                        thePartsSelectCursorController.slectedPartsInfoNum = thePartsSelectCursorController.slectedPartsInfoNum + 7;
                        Debug.Log(thePartsSelectCursorController.slectedPartsInfoNum);

                        thePartsSelectCursorController.selection_rectTransform.anchoredPosition = 
                            thePartsSelectCursorController.selection_Pos[thePartsSelectCursorController.countSelection]; 
                    }
                } 
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!theRobotPartsManager.robotPartsControllers[thePartsSelectCursorController.countSelection].isEmpty)
                    {
                        Debug.Log("This part is not empty!");
                        if (selectedPartsNum.Count < 3)
                        {
                            theSelectedPartsManager.SetPickedPartsInfo(theRobotPartsManager.randomPartsInfo[thePartsSelectCursorController.slectedPartsInfoNum]);
                            selectedPartsNum.Add(thePartsSelectCursorController.slectedPartsInfoNum);
                            theRobotPartsManager.robotPartsControllers[thePartsSelectCursorController.countSelection].CheckParts(true);
                        }
                        else
                        {
                            TextManager.instance.TryTextInfoInput("All the parts are filled. Try Trashbin");

                        }
                    }

                    else
                    {
                        TextManager.instance.TryTextInfoInput("This is empty slot");
                    }
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    for (int i = 0; i < selectedPartsNum.Count; i++)
                    {
                        
                    }
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



    private void CheckStacksForPartsCount()
    {
        if (theRobotPartsManager.countPartsStack == 1)
        {
            thePartsSelectCursorController.slectedPartsInfoNum = 0;

        }
        
        else if (theRobotPartsManager.countPartsStack == 2)
        {
            thePartsSelectCursorController.slectedPartsInfoNum = 7;

        }
        
        else if (theRobotPartsManager.countPartsStack == 3)
        {
            thePartsSelectCursorController.slectedPartsInfoNum = 14;

        }
        
        else if (theRobotPartsManager.countPartsStack == 4)
        {
            thePartsSelectCursorController.slectedPartsInfoNum = 21;
        }

    }

    private void SelectedCursorReset()
    {
        //reset selection cursor to 0 and reset pos
        thePartsSelectCursorController.countSelection = 0;
        thePartsSelectCursorController.selection_rectTransform.anchoredPosition =
            thePartsSelectCursorController.selection_Pos[thePartsSelectCursorController.countSelection];
    }


    private IEnumerator CheckSlot()
    {
        Debug.Log("CheckSlot");
        theRobotPartsManager.ResetPartsImageAlpha();
        //CheckSelectedParts();
        yield return null;
    }
}
