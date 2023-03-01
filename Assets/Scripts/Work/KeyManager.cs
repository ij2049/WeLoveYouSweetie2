using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private GameObject catalogPanel;
    [SerializeField] private GameObject btn_parts;
    [SerializeField] private GameObject obj_partsImg;
    [SerializeField] private GameObject obj_missonCompletePanel;
    [SerializeField] private FurnitureType workingFurnitureType;
    
    //Data
    private RobotPartsManager theRobotPartsManager;
    private SelectedPartsManager theSelectedPartsManager;
    private CatalogManager theCatalogManager;
    private PartsSelectCursorController thePartsSelectCursorController;
    [HideInInspector] public string currentWorkingPlayerName;
    public List<int> selectedPartsNum = new List<int>(); //the parts that is selected from the player (space)
    public List<int> usedPartsNum = new List<int>();
    
    //bool
    private bool isComplete;
    public static bool isCatalogOpened;

    private void Awake()
    {
        theRobotPartsManager = FindObjectOfType<RobotPartsManager>();
        thePartsSelectCursorController = FindObjectOfType<PartsSelectCursorController>();
        theSelectedPartsManager = FindObjectOfType<SelectedPartsManager>();
        theCatalogManager = FindObjectOfType<CatalogManager>();
        FurnitureType.isPlayerWorking = true; //delete this later it's for test
    }

    private void Update()
    {
        if (FurnitureType.isPlayerWorking)
        {
            if (!isComplete)
            { 
                if (!isCatalogOpened) 
                { 
                    //right
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        CheckSlot();
                        SelectedCursorReset();
                        
                        Debug.Log("Right");

                        //Stack counting
                        theRobotPartsManager.countPartsStack++;
                        CheckStacksForPartsCount();

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
                    //left
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        CheckSlot();

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
                    //right select part move
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                    {
                        thePartsSelectCursorController.countSelection++;
                        thePartsSelectCursorController.slectedPartsInfoNum++;//count which parts is cursor located
                        
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
                    //left select part move
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
                    //choose the part
                    if (Input.GetKeyDown(KeyCode.Space))
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
                                WorkingTextManager.instance.TryTextInfoInput("All the parts are filled. Try Trashbin");
                            }
                        }

                        else
                        {
                            WorkingTextManager.instance.TryTextInfoInput("This is empty slot");
                        }
                    }
                    //empty the part
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        WorkTrashbinReset();
                    }
                    //check the choice parts
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        CheckAssembledParts();
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
        }
        else
        {
            Debug.Log("isPlayerWorking false");
        }
        
    }

    //trashbin : put back parts from selected parts section. Reset the cursor.
    private void WorkTrashbinReset()
    {
        theSelectedPartsManager.ResetParts();
        SelectedCursorReset();
        CheckStacksForPartsCount();
        selectedPartsNum.Clear();
        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
        CheckSlot();
    }

    //current scene that is showing robot parts on the top reset
    public void ResetCurrentPartsShow()
    {
        SelectedCursorReset();
        theSelectedPartsManager.ResetParts();
        selectedPartsNum.Clear();
        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
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
    
    private void CheckSlot()
    {
        //make all the parts alpha 1(visible), all parts controller's isEmpty is now false
        theRobotPartsManager.ResetPartsImageAlpha();
    }

    private void CheckAssembledParts()
    {
        Debug.Log("check assembled parts");
        //check what parts player choose
        //check what parts that catalog have
        for (int i = 0; i < theCatalogManager.cardController.Length; i++)
        {
            //Leg
            if (theSelectedPartsManager.theSelectedPartsInfo[0] != null)
            {
                if (theSelectedPartsManager.theSelectedPartsInfo[0].partsName == theCatalogManager.cardController[i].legType)
                {
                    //body
                    if (theSelectedPartsManager.theSelectedPartsInfo[1].partsName == theCatalogManager.cardController[i].bodyType)
                    {
                        if(theSelectedPartsManager.theSelectedPartsInfo[2].partsName == theCatalogManager.cardController[i].headType)
                        {
                            if (!theCatalogManager.cardController[i].isComplete)
                            {
                                StartCoroutine(RobotAssemblePartComplete(i));
                            }
                            else
                            {
                                WorkingTextManager.instance.TryTextInfoInput("This card is already complete");
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("the selected parts manager, the selected parts info is currently null");
            }
           
        }
        //compare player's choice parts and catalog parts
        //if there is right one get rif of the card that is on the catalog(complete)
    }

    //individual part Complete
    private IEnumerator RobotAssemblePartComplete(int _num)
    {
        theCatalogManager.cardController[_num].isComplete = true;
        theCatalogManager.cardController[_num].img_Complete.SetActive(true);
        for (int i = 0; i < selectedPartsNum.Count; i++)
        {
            usedPartsNum.Add(selectedPartsNum[i]);
        }
        theCatalogManager.completeCardCount++;
        theRobotPartsManager.AddImgToEachParts(theRobotPartsManager.countPartsStack);
        theSelectedPartsManager.ResetParts();
        SelectedCursorReset();
        selectedPartsNum.Clear();
        //if the player finish the work(work complete!)
        Debug.Log("theCatalogManager.completeCardCount : " + theCatalogManager.completeCardCount + " theCatalogManager.catalogCardsAmount : " + theCatalogManager.catalogCardsAmount);
        if (theCatalogManager.completeCardCount == theCatalogManager.catalogCardsAmount)
        {
            Debug.Log("Complete!");
            isComplete = true;
            obj_missonCompletePanel.SetActive(true);
            
            //Shuffle the catalog cards for next and reset the catalog
            theCatalogManager.CompleteWork();
            usedPartsNum.Clear();
            yield return new WaitForSeconds(5f);
            
            //turn on and off the BG working objects && let player move
            workingFurnitureType.WorkDone(currentWorkingPlayerName);
            obj_missonCompletePanel.SetActive(false);

            //Add Earned Money
            MoneyManager _theMoneyManager = FindObjectOfType<MoneyManager>();
            _theMoneyManager.TryAddMoney(150);
            isComplete = false;
        }
        yield return null;
    }
    
}
