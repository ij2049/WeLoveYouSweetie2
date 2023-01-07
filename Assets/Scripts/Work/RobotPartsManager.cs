using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public class PartsInfo
{
    public string partsName; 
    public Sprite img_parts;
}

public class RobotPartsManager : MonoBehaviour
{
    [Header("Parts Data")]
    [Space (6)]
    [Tooltip("Existed parts info")]
    [SerializeField] 
    private PartsInfo[] thePartsInfo;
    

    [Tooltip("image parts")]
    public RobotPartsController[] robotPartsControllers;
    
    public List<PartsInfo> randomPartsInfo = new List<PartsInfo>();
    private PartsInfo tempParsInfo; // 1~4
    [HideInInspector]
    public int countPartsStack;
    
    //Catalog Data
    private CatalogManager theCatalogManager;
    private KeyManager theKeyManager;
    private CatalogCardController[] theCatalogCardController;

    private void Awake()
    {
        theCatalogManager = FindObjectOfType<CatalogManager>();
        theKeyManager = FindObjectOfType<KeyManager>();
    }

    public void SetCardInfoForParts()
    {
        //check which cards are picked and save as a parts
        StartCoroutine(CatalogCardInterpret());
    }

    private void ShuffleParts()
    {
        //add more random parts
        for (int i = 0; i < 4; i++)
        {
            int rnd = Random.Range(0, thePartsInfo.Length);
            randomPartsInfo.Add(thePartsInfo[rnd]);
        }

        //shuffle
        for (int i = 0; i < randomPartsInfo.Count; i++)
        {
            int rnd = Random.Range(0, randomPartsInfo.Count);
            
            tempParsInfo = randomPartsInfo[rnd];
            randomPartsInfo[rnd] = randomPartsInfo[i];
            randomPartsInfo[i] = tempParsInfo;
        }
    }

    private IEnumerator CatalogCardInterpret()
    {
        theCatalogCardController = theCatalogManager.cardController;
        
        if (theCatalogCardController != null)
        {
            for (int i = 0; i < theCatalogCardController.Length; i++)
            {
                for (int j = 0; j < thePartsInfo.Length; j++)
                {
                    if (theCatalogCardController[i].bodyType == thePartsInfo[j].partsName)
                    {
                        randomPartsInfo.Add(thePartsInfo[j]);
                    }

                    if (theCatalogCardController[i].headType == thePartsInfo[j].partsName)
                    {
                        randomPartsInfo.Add(thePartsInfo[j]);
                    }
                
                    if (theCatalogCardController[i].legType == thePartsInfo[j].partsName)
                    {
                        randomPartsInfo.Add(thePartsInfo[j]);
                    }
                } 
            }
        }

        else
        {
            Debug.Log("thecatalogcontroller null");
        }

        ShuffleParts();
        AddImgToEachParts(1);
        countPartsStack = 1;
        
        yield return null;
    }

    //_num == 1 -> less than 7(give 1-7 parts info), _num == 2 -> less than 14....etc
    public void AddImgToEachParts(int _num)
    {
        if (_num == 1)
        {
            for (int i = 0; i < robotPartsControllers.Length; i++)
            {
                robotPartsControllers[i].thePartsInfo = randomPartsInfo[i];
                robotPartsControllers[i].img_part.sprite = robotPartsControllers[i].thePartsInfo.img_parts;
                if (theKeyManager.selectedPartsNum != null)
                {
                    for (int j = 0; j < theKeyManager.selectedPartsNum.Count; j++)
                    {
                        if (i == theKeyManager.selectedPartsNum[j])
                        {
                            robotPartsControllers[i].img_part.color = new Color(1, 1, 1, 0);
                            robotPartsControllers[i].img_part.sprite = null;
                            robotPartsControllers[i].isEmpty = true;
                        }
                    }
                }
            }
        }
        
        else if (_num == 2)
        {
            for (int i = 0; i < robotPartsControllers.Length; i++)
            {
                robotPartsControllers[i].thePartsInfo = randomPartsInfo[i+7];
                robotPartsControllers[i].img_part.sprite = robotPartsControllers[i].thePartsInfo.img_parts;
                if (theKeyManager.selectedPartsNum != null)
                {
                    for (int j = 0; j < theKeyManager.selectedPartsNum.Count; j++)
                    {
                        if (i+7 == theKeyManager.selectedPartsNum[j])
                        {
                            robotPartsControllers[i].img_part.color = new Color(1, 1, 1, 0);
                            robotPartsControllers[i].img_part.sprite = null;
                            robotPartsControllers[i].isEmpty = true;
                        }
                    }
                }
            }  
        }
        
        else if (_num == 3)
        {
            for (int i = 0; i < robotPartsControllers.Length; i++)
            {
                robotPartsControllers[i].thePartsInfo = randomPartsInfo[i+14];
                robotPartsControllers[i].img_part.sprite = robotPartsControllers[i].thePartsInfo.img_parts;
                if (theKeyManager.selectedPartsNum != null)
                {
                    for (int j = 0; j < theKeyManager.selectedPartsNum.Count; j++)
                    {
                        if (i+14 == theKeyManager.selectedPartsNum[j])
                        {
                            robotPartsControllers[i].img_part.color = new Color(1, 1, 1, 0);
                            robotPartsControllers[i].img_part.sprite = null;
                            robotPartsControllers[i].isEmpty = true;
                        }
                    }
                }
            }
        }
        
        else if (_num == 4)
        {
            for (int i = 0; i < robotPartsControllers.Length; i++)
            {
                robotPartsControllers[i].thePartsInfo = randomPartsInfo[i+21];
                robotPartsControllers[i].img_part.sprite = robotPartsControllers[i].thePartsInfo.img_parts;
                if (theKeyManager.selectedPartsNum != null)
                {
                    for (int j = 0; j < theKeyManager.selectedPartsNum.Count; j++)
                    {
                        if (i+21 == theKeyManager.selectedPartsNum[j])
                        {
                            robotPartsControllers[i].img_part.color = new Color(1, 1, 1, 0);
                            robotPartsControllers[i].img_part.sprite = null;
                            robotPartsControllers[i].isEmpty = true;
                        }
                    }
                }
            } 
        }
    }

    public void ResetPartsImageAlpha()
    {
        Debug.Log("ResetPartsImageAlpha");

        for (int i = 0; i < robotPartsControllers.Length; i++)
        {
            robotPartsControllers[i].img_part.color = new Color(1, 1, 1, 1);
            robotPartsControllers[i].isEmpty = false;
        }
    }
}
