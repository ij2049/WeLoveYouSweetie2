using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAssembleManager : MonoBehaviour
{
    private bool isShuffled;
    public void Update()
    {
        if (!isShuffled)
        {
            ShuffleRobots();
        }
    }

    private void ShuffleRobots()
    {
        isShuffled = true;
        
    }
}
