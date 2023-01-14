using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPartsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Txt_EnterToComp;

    [SerializeField] private Image[] img_SelectedParts;

    [Tooltip("Auto refill. don't touch")]
    public PartsInfo[] theSelectedPartsInfo;

    //Data
    private int choosePart; // 0 == leg, 1 == body, 2 == head
    

    private void Update()
    {
        if (choosePart == 3)
        {
            Txt_EnterToComp.gameObject.SetActive(true);
        }
        else
        {
            Txt_EnterToComp.gameObject.SetActive(false);
        }
    }

    public void ResetParts()
    {
        for (int i = 0; i < img_SelectedParts.Length; i++)
        {
            img_SelectedParts[i].color = new Color(1, 1, 1, 0);
        }
        Debug.Log("choosepart : " + choosePart);
        choosePart = 0;

        theSelectedPartsInfo = new PartsInfo[3];
        
        for (int i = 0; i < img_SelectedParts.Length; i++)
        {
            img_SelectedParts[i].sprite = null;
        }
    }
    

    public void SetPickedPartsInfo(PartsInfo _partsInfo)
    {
        if (_partsInfo == null)
        {
            Debug.Log("_partsInfo is empty!");
        }
        
        if (choosePart < 3 && 0 <= choosePart)
        {
            theSelectedPartsInfo[choosePart] = _partsInfo;
            checkChooseParts(_partsInfo);
            Debug.Log(choosePart);
        }
        else
        {
            TextManager.instance.TryTextInfoInput("All the parts are filled. Try Trashbin");
            Debug.Log("choosepart : " + choosePart);
        }
    }


    
    private void checkChooseParts(PartsInfo _partsInfo) //Head? Body? Leg?
    {
        if (choosePart == 0)
        {
            img_SelectedParts[0].color = new Color(1, 1, 1, 1);
            choosePart++;
            img_SelectedParts[0].sprite = _partsInfo.img_parts;

        }

        else if (choosePart == 1)
        {
            img_SelectedParts[1].color = new Color(1, 1, 1, 1);
            choosePart++;
            img_SelectedParts[1].sprite = _partsInfo.img_parts;

        }

        else if(choosePart == 2)
        {
            img_SelectedParts[2].color = new Color(1, 1, 1, 1);
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
