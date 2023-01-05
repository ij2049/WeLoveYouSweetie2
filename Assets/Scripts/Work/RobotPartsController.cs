using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotPartsController : MonoBehaviour
{
    public Image img_part;

    [HideInInspector]
    public PartsInfo thePartsInfo;

    [HideInInspector] 
    public bool isEmpty;


    
    private RobotPartsController theRobotPartsController;

    private void Awake()
    {
        theRobotPartsController = GetComponent<RobotPartsController>();
    }

    //check this part is taken or not
    public void CheckParts(bool _isEmpty)
    {
        theRobotPartsController.isEmpty = _isEmpty;
        
        if (_isEmpty)
        {
            theRobotPartsController.img_part.sprite = null;
            theRobotPartsController.img_part.color = new Color(1, 1, 1, 0);
        }
        else
        {
            theRobotPartsController.img_part.sprite = theRobotPartsController.thePartsInfo.img_parts;
            theRobotPartsController.img_part.color = new Color(1, 1, 1, 1);

        }
    }
}
