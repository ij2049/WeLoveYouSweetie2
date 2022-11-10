using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomName;
    private MatchmakingLobbyManager theMatchmakingLobbyManager;

    private void Start()
    {
        theMatchmakingLobbyManager = FindObjectOfType<MatchmakingLobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickRoom()
    {
        theMatchmakingLobbyManager.JoinRoom(roomName.text);
    }


}
