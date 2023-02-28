using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerStatusController : MonoBehaviour
{
    
    [Header("Player Stamina")] 
    [SerializeField] private int sp;
    private float currentSp;
    private Image img_spGauge;


    private PhotonView view;
    private PlayerStatusController thePlayerStatusController;
    private PlayerManager thePlayerManager;
    private GameManager theGameManager;
    
    private void Awake()
    {
        thePlayerStatusController = GetComponent<PlayerStatusController>();
        thePlayerStatusController.thePlayerManager = FindObjectOfType<PlayerManager>();
        thePlayerStatusController.theGameManager = FindObjectOfType<GameManager>();
    }
    
    private void Start()
    {
        thePlayerStatusController.img_spGauge = thePlayerManager.GetImage_SpGauge();
        view = GetComponent<PhotonView>();
        thePlayerStatusController.currentSp = thePlayerStatusController.sp;
    }

    // Update is called once per frame
    private void Update()
    {
        if (view.IsMine && !GameManager.isGameOver)
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
            thePlayerStatusController.currentSp -= Time.deltaTime;
        }
        else
        {
            thePlayerStatusController.theGameManager.TryGameOver(false, false, false, false, true);
        }
    }

    public void FullSpStatus()
    {
        thePlayerStatusController.currentSp = thePlayerStatusController.sp;
    }

    private void GaugeUpdate()
    {
        thePlayerStatusController.img_spGauge.fillAmount = thePlayerStatusController.currentSp / thePlayerStatusController.sp;
    }
}
