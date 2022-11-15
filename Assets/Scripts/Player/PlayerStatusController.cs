using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerStatusController : MonoBehaviour
{
    
    [Header("Player Stamina")] 
    [SerializeField] private int sp;
    private int currentSp;
    [SerializeField] private int spDecreaseTime;
    private int currentSpDecreaseTime;
    private Image img_spGauge;


    private PhotonView view;
    private PlayerStatusController thePlayerStatusController;
    private PlayerManager thePlayerManager;

    private void Awake()
    {
        thePlayerStatusController = GetComponent<PlayerStatusController>();
        thePlayerManager = FindObjectOfType<PlayerManager>();
    }
    
    private void Start()
    {
        thePlayerStatusController.img_spGauge = thePlayerManager.GetImage_SpGauge();
        thePlayerStatusController.view = GetComponent<PhotonView>();
        thePlayerStatusController.currentSp = thePlayerStatusController.sp;
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerStatusController.view.IsMine)
        {
            StartStatusCount();
            GaugeUpdate();
        }
    }
    
    private void StartStatusCount()
    {
        //Sp
        if (thePlayerStatusController.currentSp > 0)
        {
            if (thePlayerStatusController.currentSpDecreaseTime <= thePlayerStatusController.spDecreaseTime)
            {
                thePlayerStatusController.currentSpDecreaseTime++;
            }
            else
            {
                thePlayerStatusController.currentSp--;
                thePlayerStatusController.currentSpDecreaseTime = 0;
            }
        }
        else
        {
            Debug.Log("Sp became 0");
        }
    }

    public void FullSpStatus()
    {
        thePlayerStatusController.currentSp = thePlayerStatusController.sp;
    }

    private void GaugeUpdate()
    {
        thePlayerStatusController.img_spGauge.fillAmount = (float)thePlayerStatusController.currentSp / thePlayerStatusController.sp;
    }
}
