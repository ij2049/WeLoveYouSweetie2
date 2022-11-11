using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using System.IO;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform[] playerSpawnPos;

    private void Start()
    {
        TryPlayerSpawn();
    }

    private void TryPlayerSpawn()
    {
        int randomNum = Random.Range(0, playerSpawnPos.Length);
        Transform spawnPoint = playerSpawnPos[randomNum];
        GameObject playerToSpawn = playerPrefabs[(int) PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, quaternion.identity);
    }
}
