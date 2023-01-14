using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum CatalogCardType
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
    //cards info
    [SerializeField] 
    private CatalogCardInfo[] cardsInfo;

    //randomize cards that is choice from cards infos
    [Tooltip("Don't touch this. It's auto put in")]
    [SerializeField] 
    private CatalogCardInfo[] randomnizeChoicesCards;

    //each of the cards that is put from the unity
    public CatalogCardController[] cardController;
    
    //reset cardInfo
    private CatalogCardInfo[] resetCardInfo;

    private CatalogCardInfo temp_cardInfo;
    private int[] tempNum = new int[8];
    public int completeCardCount;
    public int catalogCardsAmount;

    private void Awake()
    {
        resetCardInfo = cardsInfo;
    }

    private void Start()
    {
        TryCardsShuffleAndUpdate();
        completeCardCount = 0;
        catalogCardsAmount = randomnizeChoicesCards.Length;
    }

    public void TryCardsShuffleAndUpdate()
    {
        StartCoroutine(CardsShuffleAndUpdate());
    }

    private IEnumerator CardsShuffleAndUpdate()
    {
        //choose random num and pick cards
        RandomCardsChoice();
        
        //put card info to the each card controller
        for (int i = 0; i < randomnizeChoicesCards.Length; i++)
        {
            cardController[i].cardType = randomnizeChoicesCards[i].cardType;
            cardController[i].img_card = randomnizeChoicesCards[i].img_card;
            cardInfoInterpret(i, "head");
            cardInfoInterpret(i, "body");
            cardInfoInterpret(i, "leg");
        }

        for (int i = 0; i < cardController.Length; i++)
        {
            cardController[i].theCatalogCardController.theSpriteRenderer.sprite = cardController[i].theCatalogCardController.img_card;
        }

        RobotPartsManager _tempPartsManager = FindObjectOfType<RobotPartsManager>();
        //update card Shuffle info as a parts
        _tempPartsManager.SetCardInfoForParts();
        yield return null;
    }
    
    private void RandomCardsChoice()
    {
        for (int i = 0; i < cardsInfo.Length; i++)
        {
            int rnd = Random.Range(0, cardsInfo.Length);
            
            temp_cardInfo = cardsInfo[rnd];
            cardsInfo[rnd] = cardsInfo[i];
            cardsInfo[i] = temp_cardInfo;
        }

        for (int i = 0; i < 8; i++)
        {
            randomnizeChoicesCards[i] = cardsInfo[i];
        }
    }
    
    
    //change card info enum info to the string
    private void cardInfoInterpret(int _i, string _partName)
    {
        if (_partName == "head")
        {
            if (randomnizeChoicesCards[_i].head == CatalogCardType.Star_Yellow)
            {
                cardController[_i].headType = "Star_Yellow";
            }
        
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Star_Purple)
            {
                cardController[_i].headType = "Star_Purple";
            }
        
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].headType = "OppTriangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].headType = "OppTriangle_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Triangle_Green)
            {
                cardController[_i].headType = "Triangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].headType = "Triangle_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Square_Yellow)
            {
                cardController[_i].headType = "Square_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Square_Purple)
            {
                cardController[_i].headType = "Square_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].headType = "Infinite_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].headType = "Infinite_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Moon_Purple)
            {
                cardController[_i].headType = "Moon_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].headType = "Moon_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Cloud_Green)
            {
                cardController[_i].headType = "Cloud_Green";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Cloud_purple)
            {
                cardController[_i].headType = "Cloud_purple";
            }
            
            else if (randomnizeChoicesCards[_i].head == CatalogCardType.Circle_Green)
            {
                cardController[_i].headType = "Circle_Green";
            }

            else
            {
                Debug.Log("there is no right selection");
            }
        }
        
        else if (_partName == "body")
        {
            if (randomnizeChoicesCards[_i].body == CatalogCardType.Star_Yellow)
            {
                cardController[_i].bodyType = "Star_Yellow";
            }
        
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Star_Purple)
            {
                cardController[_i].bodyType = "Star_Purple";
            }
        
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].bodyType = "OppTriangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].bodyType = "OppTriangle_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Triangle_Green)
            {
                cardController[_i].bodyType = "Triangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].bodyType = "Triangle_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Square_Yellow)
            {
                cardController[_i].bodyType = "Square_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Square_Purple)
            {
                cardController[_i].bodyType = "Square_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].bodyType = "Infinite_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].bodyType = "Infinite_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Moon_Purple)
            {
                cardController[_i].bodyType = "Moon_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].bodyType = "Moon_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Cloud_Green)
            {
                cardController[_i].bodyType = "Cloud_Green";
            }
            
            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Cloud_purple)
            {
                cardController[_i].bodyType = "Cloud_purple";
            }

            else if (randomnizeChoicesCards[_i].body == CatalogCardType.Circle_Green)
            {
                cardController[_i].bodyType = "Circle_Green";
            }
            
            else
            {
                Debug.Log("there is no right selection");
            }
        }
        
        else if (_partName == "leg")
        {
            if (randomnizeChoicesCards[_i].leg == CatalogCardType.Star_Yellow)
            {
                cardController[_i].legType = "Star_Yellow";
            }
        
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Star_Purple)
            {
                cardController[_i].legType = "Star_Purple";
            }
        
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.OppTriangle_Green)
            {
                cardController[_i].legType = "OppTriangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.OppTriangle_Purple)
            {
                cardController[_i].legType = "OppTriangle_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Triangle_Green)
            {
                cardController[_i].legType = "Triangle_Green";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Triangle_Yellow)
            {
                cardController[_i].legType = "Triangle_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Square_Yellow)
            {
                cardController[_i].legType = "Square_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Square_Purple)
            {
                cardController[_i].legType = "Square_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Infinite_Purple)
            {
                cardController[_i].legType = "Infinite_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Infinite_Yellow)
            {
                cardController[_i].legType = "Infinite_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Moon_Purple)
            {
                cardController[_i].legType = "Moon_Purple";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Moon_Yellow)
            {
                cardController[_i].legType = "Moon_Yellow";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Cloud_Green)
            {
                cardController[_i].legType = "Cloud_Green";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Cloud_purple)
            {
                cardController[_i].legType = "Cloud_purple";
            }
            
            else if (randomnizeChoicesCards[_i].leg == CatalogCardType.Circle_Green)
            {
                cardController[_i].legType = "Circle_Green";
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

    public void CompleteWork()
    {
        Debug.Log("Game Complete");

        completeCardCount = 0;
        cardsInfo = resetCardInfo;
    }
}


