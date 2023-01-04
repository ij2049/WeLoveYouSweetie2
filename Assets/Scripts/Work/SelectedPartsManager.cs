using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedPartsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_Info;
    [SerializeField] private Image[] selectedParts;

    //[HideInInspector]
    public PartsInfo[] thePartsInfo;
    
    //Data
    private int choosePart; // 0 == leg, 1 == body, 2 == head


    private void Update()
    {
       
    }

    public void ResetParts()
    {
        choosePart = 0;
        thePartsInfo = null;
        for (int i = 0; i < selectedParts.Length; i++)
        {
            selectedParts[i].sprite = null;
        }
    }

    public void SetPickedPartsInfo(PartsInfo _partsInfo)
    {
        if (choosePart < 3 && 0 <= choosePart)
        {
            thePartsInfo[choosePart] = _partsInfo;
            checkChooseParts(_partsInfo);
            Debug.Log(choosePart);
        }
        else
        {
            StartCoroutine(TextInfoAdjust("All the parts are filled. Try Trashbin"));
        }
    }

    private IEnumerator TextInfoAdjust(string _txt_Info)
    {
        txt_Info.text = _txt_Info;
        yield return new WaitForSeconds(2f);
        txt_Info.text = null;
    }
    
    private void checkChooseParts(PartsInfo _partsInfo) //Head? Body? Leg?
    {
        if (choosePart == 0)
        {
            choosePart++;
            selectedParts[0].sprite = _partsInfo.img_parts;

        }

        else if (choosePart == 1)
        {
            choosePart++;
            selectedParts[1].sprite = _partsInfo.img_parts;

        }

        else if(choosePart == 2)
        {
            choosePart++;
            selectedParts[2].sprite = _partsInfo.img_parts;
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
