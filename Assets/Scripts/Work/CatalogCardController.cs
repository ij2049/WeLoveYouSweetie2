using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogCardController : MonoBehaviour
{
    public SpriteRenderer theSpriteRenderer;
    
    //the below publics are all auto filled up
    public Sprite img_card;
    public string cardType;
    public string headType;
    public string bodyType;
    public string legType;
    
    [Tooltip("Auto fill up. Don't touch")]
    public CatalogCardController theCatalogCardController;

    private void Awake()
    {
        theCatalogCardController = GetComponent<CatalogCardController>();
    }
}
