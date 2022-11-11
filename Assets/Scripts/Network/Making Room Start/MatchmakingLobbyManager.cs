using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class MatchmakingLobbyManager : MonoBehaviourPunCallbacks
{
    //Data
    [SerializeField] private JoinRoom joinRoomPrefab;
    private List<JoinRoom> joinRoomList = new List<JoinRoom>();
    [SerializeField] private Transform contentObjects;
    
    //Lobby Info
    [SerializeField] private TMP_InputField roomInputField;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject roomPanel;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private GameObject btn_play;
    [SerializeField] private string nextSceName;
    
    //Player Button
    public List<NetworkPlayerItem> playerItemList = new List<NetworkPlayerItem>();
    public NetworkPlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public float timeBtwUpdates = 1.5f;
    private float nextUpdateTime;

    
    private void Start()
    {
        JoinLobby();
    }

    private void Update()
    {
        //check how many players are in the room and if the players are two active the button
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            btn_play.SetActive(true);
        }
        else
        {
            btn_play.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        if (nextSceName != null)
        {
            PhotonNetwork.LoadLevel(nextSceName);
        }
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() {MaxPlayers = 2, BroadcastPropsChangeToAll = true});
        }
    }

    private void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBtwUpdates;
        }
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (JoinRoom room in joinRoomList)
        {
            Destroy(room.gameObject);
        }
        joinRoomList.Clear();

        foreach (RoomInfo room in list)
        {
            JoinRoom newRoom = Instantiate(joinRoomPrefab, contentObjects);
            newRoom.SetRoomName(room.Name);
            joinRoomList.Add(newRoom);
        }
    }

    public void JoinRoom(string _roomName)
    {
        PhotonNetwork.JoinRoom(_roomName);
    }
    
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        roomInputField.text = "";
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    
    //Player
    private void UpdatePlayerList()
    {
        foreach (NetworkPlayerItem item in playerItemList)
        {
            Destroy(item.gameObject);
        }
        playerItemList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            NetworkPlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }
            playerItemList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }
}
