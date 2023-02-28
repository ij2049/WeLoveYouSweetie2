using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameOverData
{
    public bool isGameOverWork; //is game over system work?
    public GameObject obj_GameOver;
    public TextMeshProUGUI txt_GameOverInfo;
    public string restartSceName;
    public GameOverTextInfo theGameOverTextInfo;
}

[System.Serializable]
public class GameOverTextInfo
{ 
    [Space(5)]
    [TextArea(4,4)] public string txt_GameOverStarve;
    [Space(5)]
    [TextArea(4,4)] public string txt_GameOverDirty;
    [Space(5)]
    [TextArea(4,4)] public string txt_GameOverNoMoney;
    [Space(5)]
    [TextArea(4,4)] public string txt_GameOverBabyNotCared;
    [Space(5)]
    [TextArea(4,4)] public string txt_GameOverParentHospitalized;
}

public class GameManager : MonoBehaviour
{
    [Space(10)] [Header("Data")] 
    public GameObject UI;

    [Space(10)] [Header("Game Over")] 
    public GameOverData theGameOverData;
    public static bool isGameOver;

    //data
    private PhotonView view;
    private GameManager theGameManager;

    
    private void Start()
    {
        view = GetComponent<PhotonView>();
        theGameManager = GetComponent<GameManager>();
    }

    //Game Over
    public void TryGameOver(bool _isStarve, bool _isHouseDirt, bool _isNoMoney, bool _isNotCared, bool _isParentsHospitalized)
    {
        if (theGameManager.theGameOverData.isGameOverWork)
        {
            if (_isStarve)
            {
                view.RPC("GameOverStarve", RpcTarget.All);
            }
        
            else if (_isHouseDirt)
            {
                view.RPC("GameOverDirty", RpcTarget.All);
            }
        
            else if (_isNoMoney)
            {
                view.RPC("GameOverNoMoney", RpcTarget.All);
            }
        
            else if (_isNotCared)
            {
                view.RPC("GameOverBabyNotCared", RpcTarget.All);
            } 
            
            else if (_isParentsHospitalized)
            {
                view.RPC("GameOverParentHospitalized", RpcTarget.All);
            } 
        }
       
    }
    
    [PunRPC]
    void GameOverStarve()
    {
        StartCoroutine(Gameover(theGameManager.theGameOverData.theGameOverTextInfo.txt_GameOverStarve));
    }
    
    [PunRPC]
    void GameOverDirty()
    {
        StartCoroutine(Gameover(theGameManager.theGameOverData.theGameOverTextInfo.txt_GameOverDirty));
    }
    
    [PunRPC]
    void GameOverNoMoney()
    {
        StartCoroutine(Gameover(theGameManager.theGameOverData.theGameOverTextInfo.txt_GameOverNoMoney));
    }
    
    [PunRPC]
    void GameOverBabyNotCared()
    {
        StartCoroutine(Gameover(theGameManager.theGameOverData.theGameOverTextInfo.txt_GameOverBabyNotCared));
    }
    
    [PunRPC]
    void GameOverParentHospitalized()
    {
        StartCoroutine(Gameover(theGameManager.theGameOverData.theGameOverTextInfo.txt_GameOverParentHospitalized));
    }

    private IEnumerator Gameover(string _txt_gameOver)
    {
        isGameOver = true;
        theGameManager.theGameOverData.txt_GameOverInfo.text = _txt_gameOver;
        theGameManager.theGameOverData.obj_GameOver.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(theGameManager.theGameOverData.restartSceName);
    }
    
    [PunRPC]
    void RestartGame()
    {
        isGameOver = false;
    }
}
