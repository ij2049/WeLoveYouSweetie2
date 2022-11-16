using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rigidBody;
    
    private Vector2 movement;
    private PhotonView view;

    public bool isPlayerUsingNomoveFurniture;
    private PlayerController thePlayerController;

    private void Awake()
    {
        thePlayerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        thePlayerController.view = GetComponent<PhotonView>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (thePlayerController.view.IsMine && !thePlayerController.isPlayerUsingNomoveFurniture)
        {
            PlayerMovement();
        }
    }
    

    private void PlayerMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        transform.position += input.normalized * moveSpeed * Time.deltaTime;
    }
}
