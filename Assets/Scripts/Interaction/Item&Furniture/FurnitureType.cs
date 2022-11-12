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
    private BabyManager theBabyManager;

    private void Awake()
    {
        theFurnitureType = GetComponent<FurnitureType>();
        theBabyManager = FindObjectOfType<BabyManager>();
    }

    public void TryFurniture(PlayerInventory _playerInvenotry)
    {
        switch (theFurnitureType.theFurniture)
        {
            case Furniture.Cradle: TryCradle(_playerInvenotry); break;
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

    private void TryCradle(PlayerInventory _playerInventory)
    {
        //if the baby is in the cradle and no one is holding baby -> Try baby hold from cradle
        if (BabyManager.IsBabyCradle && !BabyManager.IsBabyHold)
        {
            BabyManager.IsBabyCradle = false;
            BabyManager.IsBabyHold = true;
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName == "Baby")
                {
                    _playerInventory.playerItems[i].itemObject.SetActive(true);
                    for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                    {
                        if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                        {
                            theBabyManager.theBabyInfo[j].obj_baby.SetActive(false);
                        }
                    }
                    Debug.Log("Baby is Hold!");
                }
            }
        }
        //try to put baby inside he cradle
        else if (!BabyManager.IsBabyCradle && BabyManager.IsBabyHold)
        {
            BabyManager.IsBabyCradle = true;
            BabyManager.IsBabyHold = false;
            for (int i = 0; i < _playerInventory.playerItems.Length; i++)
            {
                if (_playerInventory.playerItems[i].itemsName == "Baby")
                {
                    _playerInventory.playerItems[i].itemObject.SetActive(false);
                    for (int j = 0; j < theBabyManager.theBabyInfo.Length; j++)
                    {
                        if (theBabyManager.theBabyInfo[j].babyLocationName == "Cradle")
                        {
                            theBabyManager.theBabyInfo[j].obj_baby.SetActive(true);
                        }
                    }
                    Debug.Log("Baby is now in the cradle!");
                }
            }
        }
        
    }
}
