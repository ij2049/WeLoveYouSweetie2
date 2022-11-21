using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BabyInfo
{
    public string babyLocationName;
    public GameObject obj_baby;
}

public class BabyManager : MonoBehaviour
{
    public BabyInfo[] theBabyInfo;
    public static bool isBabyHold;
    public static bool isBabyCradle;
    public static int soothingGauge;
    public float feedingGauge;

    private void Start()
    {
        isBabyCradle = true;
    }
}
