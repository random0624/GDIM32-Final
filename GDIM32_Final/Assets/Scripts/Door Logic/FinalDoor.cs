using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Inventory playerInvetory;

    private bool isOpen = false;

    void Start()
    {
        GameController.Instance.CurrentDoor.finalDoor += Spawn;
        GameController.Instance.Player.doorClickedOn += doorLogic;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfOpen();
        Debug.Log(isOpen);
    }

    private void checkIfOpen()
    {
        if(isOpen)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Spawn()
    {
        isOpen = true;
    }

    private void doorLogic()
    {
        int index = playerInvetory.selectedItemIndex;
        itemType currentItem = playerInvetory.inventoryList[index].itemType;

        if (currentItem == itemType.Key)
        {
            ///Game Over prompt
            playerInvetory.RemoveItem(currentItem);
        }
    }
}
