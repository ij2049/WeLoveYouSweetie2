using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BabyAction
{
    public string actionName;
    public GameObject actionSpeechBubble;
}

public class BabyController : MonoBehaviour
{
    private BabyController theBabyController;
    private BabyManager theBabyManager;
    public SpriteRenderer feedingGauge;
    [SerializeField] private BabyAction[] theBabyAction;

    private void Awake()
    {
        theBabyManager = FindObjectOfType<BabyManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        theBabyController = GetComponent<BabyController>();
    }

    // Update is called once per frame
    void Update()
    {
        theBabyController.feedingGauge.size = new Vector2(theBabyManager.feedingGauge,1);
        
        if (BabyStatus.isBabyHungry)
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(true);
                }
            }
        }

        else
        {
            for (int i = 0; i < theBabyController.theBabyAction.Length; i++)
            {
                if (theBabyController.theBabyAction[i].actionName == "Hungry")
                {
                    theBabyController.theBabyAction[i].actionSpeechBubble.SetActive(false);
                }
            }
        }
    }
}
