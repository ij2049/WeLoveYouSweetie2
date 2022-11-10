using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using System.IO;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    public GameObject playerPrefabs;

    [SerializeField] private Transform[] playerSpawnPos;

    private void Start()
    {
        TryPlayerSpawn();
    }

    private void TryPlayerSpawn()
    {
        int temp = Random.Range(1, 2);
        
        if (temp == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), playerSpawnPos[0].position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), playerSpawnPos[1].position, Quaternion.identity);
        }
    }
}
