using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    //Button used for creating and joining a game
    [SerializeField] private GameObject delayLoadingButton;
    [SerializeField] private GameObject delayStartButton;
    [SerializeField] private GameObject delayCancelButton;
    //numbers of player in the room at one time
    [SerializeField] private int roomSize;
    [SerializeField] private string NextSceName;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Callback function for when the first connection is
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayLoadingButton.SetActive(false);
        delayStartButton.SetActive(true);
    }

    //paired to the Quick Start Button
    public void DelayStart()
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        //First tries to join an existing room
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("delay start");
    }

    public void MatchMakingStart()
    {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        SceneManager.LoadScene(NextSceName);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    //trying to create own room
    private void CreateRoom()
    {
        Debug.Log("Creating room");
        //creating a random name for the room
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize};
        //attempting to open a new room
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... Trying again");
        CreateRoom();
    }

    public void DelayCancel()
    {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
