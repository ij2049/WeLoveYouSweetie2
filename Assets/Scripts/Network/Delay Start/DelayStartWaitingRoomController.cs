using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    //photon view for sending rpc that updates the timer
    private PhotonView myPhotonView;
    
    //scene navigation indexes
    [SerializeField] private int multiplayerSceneIndex;
    [SerializeField] private int menuSceneIndex;
    [SerializeField] private int minPlayersToStart;
    
    //text variables for holding the displays for the countdown timer and player count
    [SerializeField] private TextMeshProUGUI playerCountDisplay;
    
    //countdown timer reset variables
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float maxFullGameWaitTime;
    
    //number of players in the room outof total room size
    [SerializeField] private TextMeshProUGUI timerToStartDisplay;
    
    private int playerCount;
    private int roomSize;

    //bool values for if the timer can countdown
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;
    
    //countdown timer variables
    private float timerToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;

    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameWaitTime;
        notFullGameTimer = maxWaitTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate();
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    private void PlayerCountUpdate()
    {
        //updates player count when players join the room
        //displays player count
        //triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + ":" + roomSize;

        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }

        else
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //called whenever a new player joins the room
        PlayerCountUpdate();
        
        //send master clients countdown timer to all other players in order to sync time
        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }
    

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        //RPC for syncing the countdown timer to those that join after it has started the countdown
        timerToStartGame = timeIn;
        notFullGameTimer = timeIn;
        if (timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //called whenever a player leaves the room
        PlayerCountUpdate();
    }

    private void WaitingForMorePlayers()
    {
        //if there is only one player in the room the timer will stop and reset
        if (playerCount <= 1)
        {
            ResetTimer();
        }
        
        //when there is enough players in the room the start timer will begin counting down
        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }
        
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timerToStartGame = notFullGameTimer;
        }
        
        //format and display countdown timer
        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        //if the countdown timer reaches 0 the game will than start
        if (timerToStartGame <= 0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    private void ResetTimer()
    {
        //resets the count down timer
        timerToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    private void StartGame()
    {
        //multiplayer scene is loaded to start the game
        startingGame = true;
        if(!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        //public function paired to cancel button in waiting room scene
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
