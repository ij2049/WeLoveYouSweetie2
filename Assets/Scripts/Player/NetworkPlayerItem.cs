using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkPlayerItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerName;
    
    private Image backgrounImage;
    [SerializeField] private Color highlightColor;
    [SerializeField] private GameObject btn_leftArrow;
    [SerializeField] private GameObject btn_rightArrow;

    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Sprite[] avatars;

    private Player player;

    private void Awake()
    {
        backgrounImage = FindObjectOfType<Image>();
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        //backgrounImage.color = highlightColor;
        btn_leftArrow.SetActive(true);
        btn_rightArrow.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["[playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["[playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }

        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    private void UpdatePlayerItem(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int) player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int) player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }
    }
}