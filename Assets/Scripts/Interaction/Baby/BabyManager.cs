using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BabyInfo
{
    public string babyLocationName;
    public GameObject obj_baby;
}

public class BabyManager : MonoBehaviour
{
    public BabyInfo[] theBabyInfo;
    public static bool IsBabyHold;
    public static bool IsBabyCradle;

    private void Start()
    {
        IsBabyCradle = true;
    }
}