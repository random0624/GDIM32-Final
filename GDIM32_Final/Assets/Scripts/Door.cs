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

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Player.doorClickedOn += doorLogic;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOpen(this);
    }

    
    private void doorLogic()
    {
        int index = playerInvetory.selectedItemIndex;
        itemType currentItem = playerInvetory.inventoryList[index];

        if (currentItem == itemType.Key)
        {
            canOpen = true;
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
            store.gameObject.SetActive(false);
            doorText.gameObject.SetActive(true);
        }
        else
        {
            doorText.gameObject.SetActive(false);
        }
    }
    
}
