using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAssembleController : MonoBehaviour
{
    [SerializeField] private GameObject catalogPanel;

    private bool isCatalogOpened;
    private void Update()
    {
        if (FurnitureType.isPlayerWorking)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("Right");
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Left");
            }
            if (Input.GetKey(KeyCode.Tab))
            {
                Debug.Log("TryTab");

                if (!isCatalogOpened)
                {
                    catalogPanel.SetActive(true);
                    isCatalogOpened = true;
                }
                else
                {
                    catalogPanel.SetActive(false);
                    isCatalogOpened = false;
                }
            }
        }
        else
        {
            Debug.Log("isPlayerWorking false");

        }
    }
}
