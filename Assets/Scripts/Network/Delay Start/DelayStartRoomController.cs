using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int waitingRoomSceneIndex;

    public override void OnEnable()
    {
        //unregister to photon callback functions
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        //called when our player joins the room
        //load into waiting scene
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
