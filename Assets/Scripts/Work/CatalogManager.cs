using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatalogCardType
{
    Star_Yellow,
    Star_Purple,
    OppTriangle_Green,
    OppTriangle_Purple,
    Triangle_Green,
    Triangle_Yellow,
    Square_Yellow,
    Square_Green,
    Infinite_Purple,
    Infinite_Yellow,
    Moon_Purple,
    Moon_Yellow,
    Cloud,
}

[System.Serializable]
public class CatalogCardInfo
{
    public string cardType; 
    public Sprite img_card;
    public CatalogCardType head;
    public CatalogCardType body;
    public CatalogCardType leg;
}


public class CatalogManager : MonoBehaviour
{
    [SerializeField] 
    private CatalogCardInfo[] cardsInfo;

    [SerializeField] 
    private CatalogCardController[] cardController;
    private void Start()
    {
        //put card info to the each card
        
        //instantiate cards img on the list

        for (int i = 0; i < cardsInfo.Length; i++)
        {
            cardInfoInterpret(i, "head");
            cardInfoInterpret(i, "body");
            cardInfoInterpret(i, "leg");
        }
    }
    
    //change card info enum info to the string
    private void cardInfoInterpret(int _i, string _partName)
    {
        if (_partName == "head")
        {
            if (cardsInfo[_i].head == CatalogCardType.Star_Yellow)
            {
                cardController[_i].headType = "Star_Yellow";
            }
        
            else if (cardsInfo[_i].head == CatalogCardType.Star_Purple)
            {
                cardController[_i].headType = "Star_Purple";
            }
        
            else if (cardsInfo[_i].head == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].headType = "OppTriangle_Green";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].headType = "OppTriangle_Purple";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Triangle_Green)
            {
                cardController[_i].headType = "Triangle_Green";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].headType = "Triangle_Yellow";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Square_Yellow)
            {
                cardController[_i].headType = "Square_Yellow";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Square_Green)
            {
                cardController[_i].headType = "Square_Green";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].headType = "Infinite_Purple";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].headType = "Infinite_Yellow";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Moon_Purple)
            {
                cardController[_i].headType = "Moon_Purple";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].headType = "Moon_Yellow";
            }
            
            else if (cardsInfo[_i].head == CatalogCardType.Cloud)
            {
                cardController[_i].headType = "Cloud";
            }

            else
            {
                Debug.Log("there is no right selection");
            }
        }
        
        else if (_partName == "body")
        {
            if (cardsInfo[_i].body == CatalogCardType.Star_Yellow)
            {
                cardController[_i].bodyType = "Star_Yellow";
            }
        
            else if (cardsInfo[_i].body == CatalogCardType.Star_Purple)
            {
                cardController[_i].bodyType = "Star_Purple";
            }
        
            else if (cardsInfo[_i].body == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].bodyType = "OppTriangle_Green";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].bodyType = "OppTriangle_Purple";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Triangle_Green)
            {
                cardController[_i].bodyType = "Triangle_Green";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].bodyType = "Triangle_Yellow";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Square_Yellow)
            {
                cardController[_i].bodyType = "Square_Yellow";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Square_Green)
            {
                cardController[_i].bodyType = "Square_Green";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].bodyType = "Infinite_Purple";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].bodyType = "Infinite_Yellow";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Moon_Purple)
            {
                cardController[_i].bodyType = "Moon_Purple";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].bodyType = "Moon_Yellow";
            }
            
            else if (cardsInfo[_i].body == CatalogCardType.Cloud)
            {
                cardController[_i].bodyType = "Cloud";
            }

            else
            {
                Debug.Log("there is no right selection");
            }
        }
        
        else if (_partName == "leg")
        {
            if (cardsInfo[_i].leg == CatalogCardType.Star_Yellow)
            {
                cardController[_i].legType = "Star_Yellow";
            }
        
            else if (cardsInfo[_i].leg == CatalogCardType.Star_Purple)
            {
                cardController[_i].legType = "Star_Purple";
            }
        
            else if (cardsInfo[_i].leg == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].legType = "OppTriangle_Green";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].legType = "OppTriangle_Purple";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Triangle_Green)
            {
                cardController[_i].legType = "Triangle_Green";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].legType = "Triangle_Yellow";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Square_Yellow)
            {
                cardController[_i].legType = "Square_Yellow";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Square_Green)
            {
                cardController[_i].legType = "Square_Green";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].legType = "Infinite_Purple";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].legType = "Infinite_Yellow";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Moon_Purple)
            {
                cardController[_i].legType = "Moon_Purple";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].legType = "Moon_Yellow";
            }
            
            else if (cardsInfo[_i].leg == CatalogCardType.Cloud)
            {
                cardController[_i].legType = "Cloud";
            }

            else
            {
                Debug.Log("there is no right selection");
            }
        }

        else
        {
            Debug.LogError("card part type is not right");
        }
    }
    
}


