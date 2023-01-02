using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PartsType
{
    Star_Yellow,
    Star_Purple,
    OppTriangle_Green,
    OppTriangle_Purple,
    Triangle_Green,
    Triangle_Yellow,
    Square_Yellow,
    Square_Purple,
    Infinite_Purple,
    Infinite_Yellow,
    Moon_Purple,
    Moon_Yellow,
    Cloud_Green,
    Cloud_purple,
    Circle_Green,
}

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
    [SerializeField]
    private RobotPartsController[] robotPartsControllers;
    
    public List<PartsInfo> randomPartsInfo = new List<PartsInfo>();
    private PartsInfo tempParsInfo;
    
    //Catalog Data
    private CatalogManager theCatalogManager;
    private CatalogCardController[] theCatalogCardController;

    private void Awake()
    {
        theCatalogManager = FindObjectOfType<CatalogManager>();
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
        yield return null;
    }
}
