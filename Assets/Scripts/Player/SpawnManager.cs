using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using System.IO;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs;
    public Transform[] playerSpawnPos;

    private void Start()
    {
        TryPlayerSpawn();
    }

    private void TryPlayerSpawn()
    {
        int randomNum = Random.Range(0, playerSpawnPos.Length);
        Transform spawnPoint = playerSpawnPos[randomNum];
        GameObject playerToSpawn;
        if (PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] == null)
        {
            playerToSpawn = playerPrefabs[0];
        }
        else
        {
            playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        }
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, quaternion.identity);
    }
}