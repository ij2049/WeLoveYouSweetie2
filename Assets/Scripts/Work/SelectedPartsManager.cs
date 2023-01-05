using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPartsManager : MonoBehaviour
{
    [SerializeField] private Image[] img_SelectedParts;

    //[HideInInspector]
    public PartsInfo[] theSelectedPartsInfo;
    
    //Data
    private int choosePart; // 0 == leg, 1 == body, 2 == head
    
    public void ResetParts()
    {
        choosePart = 0;
        theSelectedPartsInfo = null;
        for (int i = 0; i < img_SelectedParts.Length; i++)
        {
            img_SelectedParts[i].sprite = null;
        }
    }

    public void SetPickedPartsInfo(PartsInfo _partsInfo)
    {
        if (choosePart < 3 && 0 <= choosePart)
        {
            theSelectedPartsInfo[choosePart] = _partsInfo;
            checkChooseParts(_partsInfo);
            Debug.Log(choosePart);
        }
        else
        {
            TextManager.instance.TryTextInfoInput("All the parts are filled. Try Trashbin");
        }
    }


    
    private void checkChooseParts(PartsInfo _partsInfo) //Head? Body? Leg?
    {
        if (choosePart == 0)
        {
            choosePart++;
            img_SelectedParts[0].sprite = _partsInfo.img_parts;

        }

        else if (choosePart == 1)
        {
            choosePart++;
            img_SelectedParts[1].sprite = _partsInfo.img_parts;

        }

        else if(choosePart == 2)
        {
            choosePart++;
            img_SelectedParts[2].sprite = _partsInfo.img_parts;
        }

        else
        {
            if (choosePart < 0)
            {
                choosePart = 0;
            }
            
            else if (2 < choosePart)
            {
                choosePart = 3;
            }
                
        }
    }
}
