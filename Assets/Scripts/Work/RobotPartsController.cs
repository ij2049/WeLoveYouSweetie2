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

    private RobotPartsController theRobotPartsController;

    private void Awake()
    {
        theRobotPartsController = GetComponent<RobotPartsController>();
    }
}
