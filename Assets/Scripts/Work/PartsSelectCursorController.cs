using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PartsSelectCursorController : MonoBehaviour
{
    public GameObject obj_partSelection;

    public Vector3[] selection_Pos;
    public RectTransform selection_rectTransform;
    [HideInInspector] public int countSelection;
    [HideInInspector] public int slectedPartsInfoNum;
}
