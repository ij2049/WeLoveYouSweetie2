using System;
using System.Collections;
using System.Collections.Generic;
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
    
    ExitGames.Client.Photon

    private void Start()
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
}