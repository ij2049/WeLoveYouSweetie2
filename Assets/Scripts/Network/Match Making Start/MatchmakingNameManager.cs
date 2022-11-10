using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchmakingNameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI btn_txt;
    [SerializeField] private string nextSceName;

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            btn_txt.text = "Connecting..";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(nextSceName);
    }
}
