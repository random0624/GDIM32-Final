using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool canOpen;

    [SerializeField] private Inventory playerInvetory;
    [SerializeField] private TMP_Text doorText;

    public delegate void ListenToEvent();
    public event ListenToEvent wrongKey;
    public event ListenToEvent correctKey;

    private int doorsOponed;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.doorClickedOn += doorLogic;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOpen(this);
       // Debug.Log(canOpen);
    }
    
    
    private void doorLogic()
    {
        int index = playerInvetory.selectedItemIndex; 
        itemType currentItem = playerInvetory.inventoryList[index].itemType;

        if (currentItem == itemType.Key)
        {
            canOpen = true;
            playerInvetory.RemoveItem(currentItem);
            correctKey?.Invoke();

        } else if (index == 0)
        {
            Debug.Log("You dont have an item");
        }
        else
        {
           wrongKey?.Invoke();
        }
    }

    private void CheckIfOpen(Door store)
    {
        if (canOpen == true)
        {
            if (doorsOponed >= 3)
            {
                store.gameObject.SetActive(false);
            }
            else
            {
                canOpen = false;
                store.gameObject.transform.position = this.transform.position + Vector3.forward / 2;
                doorsOponed++;
            }
            doorText.gameObject.SetActive(true);
        }
        else
        {
            doorText.gameObject.SetActive(false);
        }
    }
    
}
