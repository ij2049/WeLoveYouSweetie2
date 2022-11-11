using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Furniture
{
    Cradle,
    Bathtub,
    Trash
}

public class FurnitureType : MonoBehaviour
{
    [SerializeField] private Furniture theFurniture;
    private FurnitureType theFurnitureType;
    private string furnitureInfo;

    private void Awake()
    {
        theFurnitureType = GetComponent<FurnitureType>();
    }

    public void TryFurniture(PlayerInventory _playerInvenotry)
    {
        switch (theFurnitureType.theFurniture)
        {
            case Furniture.Cradle: theFurnitureType.furnitureInfo = "Cradle"; break;
            case Furniture.Bathtub: theFurnitureType.furnitureInfo = "Bathtub"; break;
            case Furniture.Trash: TryTrashbin(_playerInvenotry); break;
        }
    }

    private void TryTrashbin(PlayerInventory _playerInventory)
    {
        Debug.Log("Try Trashbin!");

        if (PlayerInventory.isHolding)
        {
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                _playerInventory.playerItems[i].itemObject.SetActive(false);
            }

            PlayerInventory.isHolding = false;
        }

        else
        {
            Debug.Log("Player is not holding");
        }
    }
}
