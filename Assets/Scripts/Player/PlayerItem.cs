using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    
    private Image backgrounImage;
    [SerializeField] private Color highlightColor;
    [SerializeField] private GameObject btn_leftArrow;
    [SerializeField] private GameObject btn_rightArrow;

    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] private Image playerAvatar;
    [SerializeField] private Sprite[] avatars;

    private void Awake()
    {
        backgrounImage = FindObjectOfType<Image>();
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }

    public void ApplyLocalChanges()
    {
        backgrounImage.color = highlightColor;
        btn_leftArrow.SetActive(true);
        btn_rightArrow.SetActive(true);
    }

    public void OnClickLeftArrow()
    {
        if ((int) playerProperties["playerAvatar"] == 0)
        {
            playerProperties["[playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
    }

    public void OnClickRightArrow()
    {
        if ((int) playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["[playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
    }
}